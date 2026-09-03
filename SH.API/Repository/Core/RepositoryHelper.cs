using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.Shared.Core.Types;

namespace SH.API.Repository.Core
{
    public static class RepositoryHelper
    {
        public static async Task<ProfileModel> GetProfile(this HttpRequestData req, CosmosMainRepository repo, CancellationToken cancellationToken)
        {
            var userId = await req.GetUserIdAsync();

            return await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");
        }

        public static async Task<ProfileProperty> GetProfileProperty(this HttpRequestData req, CosmosMainRepository repo, string? propertyId, ProfileModel? profile, CancellationToken cancellationToken)
        {
            profile ??= await req.GetProfile(repo, cancellationToken);

            return profile.Properties.SingleOrDefault(p => string.Equals(p.PropertyId, propertyId, StringComparison.Ordinal)) ?? throw new NotificationException("Your profile is not linked to this property.");
        }

        public static async Task<bool> IsPropertyPublic(this HttpRequestData req, CosmosMainRepository repo, string? propertyId, CancellationToken cancellationToken)
        {
            var profileProperty = await req.GetProfileProperty(repo, propertyId, profile: null, cancellationToken);

            return profileProperty.IsPublic;
        }
    }
}