using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using SH.API.Core.Auth;
using SH.API.Repository.Core;
using SH.Shared.Core.Types;
using SH.Shared.Models.Index;
using SH.Shared.Models.Property;
using SH.Shared.Models.Property.Core;
using static SH.Shared.Core.Helper.ImageHelper;

namespace SH.API.Functions.Auth;

public class PropertyFunction(CosmosMainRepository repoMain, [FromKeyedServices("Off")] CosmosPropertyRepository repoOff, [FromKeyedServices("On")] CosmosPropertyRepository repoOn, StorageHelper storageHelper)
{
    private CosmosPropertyRepository GetRepo(bool isPublic)
    {
        return isPublic ? repoOn : repoOff;
    }

    [Function("PropertyGet")]
    public async Task<HttpResponseData?> PropertyGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "property/get/{propertyId}")] HttpRequestData req, string propertyId, CancellationToken cancellationToken)
    {
        var isPublic = await req.IsPropertyPublic(repoMain, propertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var model = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }

    [Function("PropertyAdd")]
    public async Task<PropertyModel?> PropertyAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/add")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync();

        var body = await req.GetBody<PropertyModel>(cancellationToken);

        var profile = await repoMain.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("profie not found");
        var property = new ProfileProperty()
        {
            PropertyId = body.Identity.RawId,
            Title = body.Title,
            PhotoId = body.Photos.FirstOrDefault()?.PhotoId,
            Role = PropertyUserRole.Owner,
        };

        profile.Properties.Add(property);

        var countryIndex = await repoMain.ReadItemAsync<CountryIndex>(new MainIdentity(MainType.CountryIndex, "all"), cancellationToken);
        countryIndex ??= new CountryIndex("all");

        var country = countryIndex.Countries.FirstOrDefault(c => string.Equals(c.CountryCode, body.CountryCode, StringComparison.Ordinal)) ?? new CountryIndexItem(body.CountryCode, body.Address.CountryName);

        var countryChanged = countryIndex.Countries.Add(country);

        if (countryChanged)
        {
            await repoMain.UpsertItemAsync(countryIndex);
        }

        var stateIndex = await repoMain.ReadItemAsync<StateIndex>(new MainIdentity(MainType.StateIndex, body.CountryCode), cancellationToken);
        stateIndex ??= new StateIndex(body.CountryCode);

        var state = stateIndex.States.FirstOrDefault(s => string.Equals(s.StateCode, body.StateCode, StringComparison.Ordinal)) ?? new StateIndexItem(body.StateCode, body.Address.State);
        var city = state.Cities.FirstOrDefault(c => string.Equals(c.CityCode, body.CityCode, StringComparison.Ordinal)) ?? new CityIndexItem(body.CityCode, body.Address.City);

        var stateChanged = stateIndex.States.Add(state);
        var citychanged = state.Cities.Add(city);

        if (stateChanged || citychanged)
        {
            await repoMain.UpsertItemAsync(stateIndex);
        }

        await repoMain.UpsertItemAsync(profile);

        var repo = GetRepo(isPublic: false);
        return await repo.CreateItemAsync(body);
    }

    [Function("PropertyUpdate")]
    public async Task<PropertyModel?> PropertyUpdate(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/update")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var body = await req.GetBody<PropertyModel>(cancellationToken);

        var profile = await req.GetProfile(repoMain, cancellationToken);

        //todo: change properties of all members
        var profileProperty = await req.GetProfileProperty(repoMain, body.Identity.RawId, profile, cancellationToken);

        profileProperty.Title = body.Title;
        profileProperty.PhotoId = body.Photos.FirstOrDefault()?.PhotoId;

        await repoMain.UpsertItemAsync(profile);

        var repo = GetRepo(profileProperty.IsPublic);
        return await repo.UpsertItemAsync(body);
    }

    [Function("PropertyDelete")]
    public async Task<ProfileModel> PropertyDelete(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/delete/{propertyId}")] HttpRequestData req, string propertyId, CancellationToken cancellationToken)
    {
        var isPublic = await req.IsPropertyPublic(repoMain, propertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var profile = await req.GetProfile(repoMain, cancellationToken);

        //todo: remove properties of all members
        var profileProperty = await req.GetProfileProperty(repoMain, propertyId, profile, cancellationToken);

        profile.Properties.Remove(profileProperty);

        var model = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken) ?? throw new NotificationException("Property not found");

        foreach (var item in model.Rooms)
        {
            await repo.DeleteItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, item.RoomId));

            //todo: delete photos
        }

        foreach (var item in model.Photos)
        {
            await storageHelper.DeletePhoto(PhotoType.Gallery, $"{item.PhotoId}.jpg", cancellationToken);
        }

        await repo.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId));

        return await repoMain.UpsertItemAsync(profile);
    }

    [Function("PropertyPublicMode")]
    public async Task<ProfileModel> PropertyPublicMode(
     [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/public/{propertyId}")] HttpRequestData req, string propertyId, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync();

        var property = await repoOff.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken) ?? throw new NotificationException("property not found");

        var ProfileValidator = new PropertyValidation();

        var validation = await ProfileValidator.ValidateAsync(property, options => options.IncludeAllRuleSets(), cancellationToken);

        if (!validation.IsValid)
        {
            throw new NotificationException(validation.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid property data.");
        }

        await repoOn.CreateItemAsync(property);
        await repoOff.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId));

        foreach (var room in property.Rooms)
        {
            var model = await repoOff.ReadItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, room.RoomId), cancellationToken) ?? throw new NotificationException("Room not found");
            await repoOn.CreateItemAsync(model);
            await repoOff.DeleteItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, model.Id));
        }

        //todo: update all members of the property to be public
        var profile = await repoMain.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new UnhandledException("Profile is null");
        var prop = profile.Properties.Single(p => string.Equals(p.PropertyId, propertyId, StringComparison.Ordinal));
        prop.IsPublic = true;

        return await repoMain.UpsertItemAsync(profile);
    }

    [Function("PropertyPrivateMode")]
    public async Task<ProfileModel> PropertyPrivateMode(
     [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/private/{propertyId}")] HttpRequestData req, string propertyId, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync();

        var property = await repoOn.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken) ?? throw new NotificationException("property not found");

        await repoOff.CreateItemAsync(property);
        await repoOn.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId));

        //todo: update all members of the property to be private
        var profile = await repoMain.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new UnhandledException("Profile is null");
        var prop = profile.Properties.Single(p => string.Equals(p.PropertyId, propertyId, StringComparison.Ordinal));
        prop.IsPublic = false;

        return await repoMain.UpsertItemAsync(profile);
    }
}