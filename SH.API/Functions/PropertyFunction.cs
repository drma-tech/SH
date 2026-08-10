using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.Shared.Core.Types;
using SH.Shared.Models.Property;

namespace SH.API.Functions;

public class PropertyFunction(CosmosMainRepository repo, CosmosPropertyOffRepository repoOff, CosmosPropertyOnRepository repoOn)
{
    [Function("PropertyGet")]
    public async Task<HttpResponseData?> PropertyGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "property/get/{live}/{id}")] HttpRequestData req, bool live, string id, CancellationToken cancellationToken)
    {
        var model = live ? null : await repoOff.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id), cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }

    [Function("PropertyAdd")]
    public async Task<PropertyModel?> PropertyAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/add")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetBody<PropertyModel>(cancellationToken);

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("profie not found");
        var property = new ProfileProperty()
        {
            PropertyId = model.Identity.RawId,
            Title = model.Title,
            UrlPath = model.Photos.FirstOrDefault()?.UrlPath,
            Role = PropertyUserRole.Owner,
        };

        profile.Properties.Add(property);

        await repo.UpsertItemAsync(profile);
        return await repoOff.CreateItemAsync(model);
    }

    [Function("PropertyUpdate")]
    public async Task<PropertyModel?> PropertyUpdate(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Put, Route = "property/update")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetBody<PropertyModel>(cancellationToken);

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        var property = profile.Properties.SingleOrDefault(p => string.Equals(p.PropertyId, model.Identity.RawId, StringComparison.Ordinal)) ?? throw new InvalidOperationException("Your profile is not linked to this property.");

        property.Title = model.Title;
        property.UrlPath = model.Photos.FirstOrDefault()?.UrlPath;

        await repo.UpsertItemAsync(profile);
        return await repoOff.UpsertItemAsync(model);
    }

    [Function("PropertyDelete")]
    public async Task<ProfileModel> PropertyDelete(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/delete/{live}/{id}")] HttpRequestData req, bool live, string id, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        if (profile.Properties.ToHashSet().RemoveWhere(p => string.Equals(p.PropertyId, id, StringComparison.Ordinal)) != 1)
        {
            throw new InvalidOperationException("Your profile is not linked to this property.");
        }

        if (live)
        {
            //todo: implement live repository
        }
        else
        {
            await repoOff.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id));
        }

        return await repo.UpsertItemAsync(profile);
    }

    [Function("PropertyPublicMode")]
    public async Task<ProfileModel> PropertyPublicMode(
     [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/public/{id}")] HttpRequestData req, string id, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);

        var property = await repoOff.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id), cancellationToken) ?? throw new NotificationException("property not found");
        //var ProfileValidator = new PropertyValidation();
        //var ProfileValid = (await ProfileValidator.ValidateAsync(profile, options => options.IncludeAllRuleSets(), cancellationToken)).IsValid;

        //if (profile.MaritalStatus == MaritalStatus.Married || profile.MaritalStatus == MaritalStatus.CommonLawCohabiting)
        //{
        //    throw new NotificationException(
        //        "You cannot make your profile public while in a committed relationship. Once your status changes, you can activate it."
        //    );
        //}

        //var filter = await repo.ReadItemAsync<FilterModel>(new MainIdentity(MainType.Filter, userId), cancellationToken) ?? throw new NotificationException("filter not found");
        //var FilterValidator = new FilterValidation();
        //var FilterValid = filter != null && (await FilterValidator.ValidateAsync(filter, cancellationToken)).IsValid;

        //var setting = await repo.ReadItemAsync<SettingModel>(new MainIdentity(MainType.Setting, userId), cancellationToken) ?? throw new NotificationException("setting not found");
        //var SettingValid = setting != null;

        //var PhotoValidator = new PhotoValidation();
        //var GalleryValid = profile.Gallery != null && (await PhotoValidator.ValidateAsync(profile.Gallery, cancellationToken)).IsValid;

        //var validation = await repo.ReadItemAsync<ValidationModel>(new MainIdentity(MainType.Validation, userId), cancellationToken) ?? throw new NotificationException("validation not found");
        //var ValidationsValid = validation != null && validation.Gallery;

        //if (!ProfileValid || !FilterValid || !SettingValid || !GalleryValid || !ValidationsValid)
        //{
        //    throw new NotificationException("Please complete all steps before making your profile public.");
        //}

        await repoOn.CreateItemAsync(property);
        await repoOff.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id));

        //todo: update all members of the property to be public
        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new UnhandledException("Profile is null");
        var prop = profile.Properties.Single(p => string.Equals(p.PropertyId, id, StringComparison.Ordinal));
        prop.IsPublic = true;

        return await repo.UpsertItemAsync(profile);
    }

    [Function("PropertyPrivateMode")]
    public async Task<ProfileModel> PropertyPrivateMode(
     [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/private/{id}")] HttpRequestData req, string id, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);

        var property = await repoOn.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id), cancellationToken) ?? throw new NotificationException("property not found");
        //var ProfileValidator = new PropertyValidation();
        //var ProfileValid = (await ProfileValidator.ValidateAsync(profile, options => options.IncludeAllRuleSets(), cancellationToken)).IsValid;

        //if (profile.MaritalStatus == MaritalStatus.Married || profile.MaritalStatus == MaritalStatus.CommonLawCohabiting)
        //{
        //    throw new NotificationException(
        //        "You cannot make your profile public while in a committed relationship. Once your status changes, you can activate it."
        //    );
        //}

        //var filter = await repo.ReadItemAsync<FilterModel>(new MainIdentity(MainType.Filter, userId), cancellationToken) ?? throw new NotificationException("filter not found");
        //var FilterValidator = new FilterValidation();
        //var FilterValid = filter != null && (await FilterValidator.ValidateAsync(filter, cancellationToken)).IsValid;

        //var setting = await repo.ReadItemAsync<SettingModel>(new MainIdentity(MainType.Setting, userId), cancellationToken) ?? throw new NotificationException("setting not found");
        //var SettingValid = setting != null;

        //var PhotoValidator = new PhotoValidation();
        //var GalleryValid = profile.Gallery != null && (await PhotoValidator.ValidateAsync(profile.Gallery, cancellationToken)).IsValid;

        //var validation = await repo.ReadItemAsync<ValidationModel>(new MainIdentity(MainType.Validation, userId), cancellationToken) ?? throw new NotificationException("validation not found");
        //var ValidationsValid = validation != null && validation.Gallery;

        //if (!ProfileValid || !FilterValid || !SettingValid || !GalleryValid || !ValidationsValid)
        //{
        //    throw new NotificationException("Please complete all steps before making your profile public.");
        //}

        await repoOff.CreateItemAsync(property);
        await repoOn.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id));

        //todo: update all members of the property to be private
        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new UnhandledException("Profile is null");
        var prop = profile.Properties.Single(p => string.Equals(p.PropertyId, id, StringComparison.Ordinal));
        prop.IsPublic = false;

        return await repo.UpsertItemAsync(profile);
    }
}