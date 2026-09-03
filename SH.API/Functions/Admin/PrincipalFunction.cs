using Clerk.BackendAPI;
using Clerk.BackendAPI.Models.Operations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.Shared.Core.Types;
using SH.Shared.Models.Auth;

namespace SH.API.Functions.Admin;

public class PrincipalFunction(CosmosMainRepository repo)
{
    //private const string CloneFailed = "DeepClone failed";

    //[Function("PrincipalMigrate")]
    //public async Task PrincipalMigrate(
    //    [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "principal/migrate")] HttpRequestData req, CancellationToken cancellationToken)
    //{
    //    var principais = await repo.Query<AuthPrincipal>(MainType.Principal, p => !p.UserId!.StartsWith("user_"), transform: null, cancellationToken);
    //    var sdk = new ClerkBackendApi(bearerAuth: ApiStartup.Configurations.ClerkAuth!.SecretKey);

    //    foreach (var principal in principais)
    //    {
    //        try
    //        {
    //            var request = new CreateUserRequestBody()
    //            {
    //                FirstName = principal.DisplayName?.Split(" ").ElementAtIndex(0),
    //                LastName = principal.DisplayName?.Split(" ").ElementAtIndex(1),
    //                EmailAddress = [principal.Email!],
    //            };

    //            var user = await sdk.Users.CreateAsync(request);

    //            var clone = principal.DeepClone() ?? throw new NotificationException(CloneFailed);
    //            clone.ChangeIdentity(new MainIdentity(MainType.Principal, user.User!.Id));
    //            clone.UserId = user.User.Id;
    //            await repo.CreateItemAsync(clone);
    //            await repo.DeleteItemAsync<AuthPrincipal>(new MainIdentity(MainType.Principal, principal.Id));

    //            var myLogins = await repo.ReadItemAsync<AuthLogin>(new MainIdentity(MainType.Login, principal.Id), cancellationToken);
    //            if (myLogins != null)
    //            {
    //                var model = myLogins.DeepClone() ?? throw new NotificationException(CloneFailed);
    //                model.ChangeIdentity(new MainIdentity(MainType.Login, user.User.Id));
    //                model.UserId = user.User.Id;
    //                await repo.CreateItemAsync(model);
    //                await repo.DeleteItemAsync<AuthLogin>(new MainIdentity(MainType.Login, principal.Id));
    //            }

    //            var myProfile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, principal.Id), cancellationToken);
    //            if (myProfile != null)
    //            {
    //                var model = myProfile.DeepClone() ?? throw new NotificationException(CloneFailed);
    //                model.ChangeIdentity(new MainIdentity(MainType.Profile, user.User.Id));
    //                await repo.CreateItemAsync(model);
    //                await repo.DeleteItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, principal.Id));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
}