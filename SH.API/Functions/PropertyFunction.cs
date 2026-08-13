using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.API.Repository.Core;
using SH.Shared.Core.Types;
using SH.Shared.Models.Index;
using SH.Shared.Models.Property;
using SH.Shared.Requests;

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

    [Function("PropertySearch")]
    public async Task<HttpResponseData?> PropertySearch(
       [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/search")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var request = await req.GetRequest<PropertySearchRequest>(cancellationToken) ?? throw new NotificationException("invalid request");
        var predicate = ExpressionExtensions.CreatePredicate<PropertyModel>();

        if (request.Country.NotEmpty()) predicate = predicate.AndAlso(p => p.CountryCode == request.Country);
        if (request.State.NotEmpty()) predicate = predicate.AndAlso(p => p.StateCode == request.State);
        if (request.City.NotEmpty()) predicate = predicate.AndAlso(p => p.CityCode == request.City);

        var transform = TransformExtensions.CreateTransform<PropertyModel>()
            .Skip(request.Skip)
            .Take(request.Take)
            .OrderByDescending(p => p.DateTimeCreated);

        var result = await repoOff.Query(PropertyType.Property, predicate, transform, cancellationToken);

        return await req.CreateResponse(result, TtlCache.OneDay, cancellationToken);
    }

    [Function("PropertyAdd")]
    public async Task<PropertyModel?> PropertyAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/add")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var body = await req.GetBody<PropertyModel>(cancellationToken);

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("profie not found");
        var property = new ProfileProperty()
        {
            PropertyId = body.Identity.RawId,
            Title = body.Title,
            UrlPath = body.Photos.FirstOrDefault()?.UrlPath,
            Role = PropertyUserRole.Owner,
        };

        profile.Properties.Add(property);

        var countryIndex = await repo.ReadItemAsync<CountryIndex>(new MainIdentity(MainType.CountryIndex, "all"), cancellationToken);
        countryIndex ??= new CountryIndex("all");

        var country = countryIndex.Countries.FirstOrDefault(c => string.Equals(c.CountryCode, body.CountryCode, StringComparison.Ordinal)) ?? new CountryIndexItem(body.CountryCode, body.Address.CountryName);

        var countryChanged = countryIndex.Countries.Add(country);

        if (countryChanged)
        {
            await repo.UpsertItemAsync(countryIndex);
        }

        var stateIndex = await repo.ReadItemAsync<StateIndex>(new MainIdentity(MainType.StateIndex, body.CountryCode), cancellationToken);
        stateIndex ??= new StateIndex(body.CountryCode);

        var state = stateIndex.States.FirstOrDefault(s => string.Equals(s.StateCode, body.StateCode, StringComparison.Ordinal)) ?? new StateIndexItem(body.StateCode, body.Address.State);
        var city = state.Cities.FirstOrDefault(c => string.Equals(c.CityCode, body.CityCode, StringComparison.Ordinal)) ?? new CityIndexItem(body.CityCode, body.Address.City);

        var stateChanged = stateIndex.States.Add(state);
        var citychanged = state.Cities.Add(city);

        if (stateChanged || citychanged)
        {
            await repo.UpsertItemAsync(stateIndex);
        }

        await repo.UpsertItemAsync(profile);
        return await repoOff.CreateItemAsync(body);
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
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/delete/{id}")] HttpRequestData req, string id, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: remove properties of all members
        var property = profile.Properties.SingleOrDefault(p => string.Equals(p.PropertyId, id, StringComparison.Ordinal)) ?? throw new InvalidOperationException("Your profile is not linked to this property.");

        //todo: delete photos and rooms

        profile.Properties.Remove(property);

        if (property.IsPublic)
        {
            await repoOn.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id));
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