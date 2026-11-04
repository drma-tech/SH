using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using SH.API.Repository.Core;
using SH.Shared.Core.Types;
using SH.Shared.Models.Property;
using SH.Shared.Requests;

namespace SH.API.Functions.Public;

public class PropertyPublicFunction([FromKeyedServices("On")] CosmosPropertyRepository repoOn)
{
    [Function("PropertyView")]
    public async Task<HttpResponseData?> PropertyView(
       [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "property/view/{propertyId}")] HttpRequestData req, string propertyId, CancellationToken cancellationToken)
    {
        var model = await repoOn.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken) ?? throw new NotificationException("property not found");

        model.Address = new();

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

        var result = await repoOn.Query(PropertyType.Property, predicate, transform, cancellationToken);

        return await req.CreateResponse(result, TtlCache.OneDay, cancellationToken);
    }

    [Function("RoomView")]
    public async Task<HttpResponseData?> RoomView(
      [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "room/view/{roomId}")] HttpRequestData req, string roomId, CancellationToken cancellationToken)
    {
        var model = await repoOn.ReadItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, roomId), cancellationToken) ?? throw new NotificationException("room not found");

        //model.Address = new();

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }
}