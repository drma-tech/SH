using SH.Shared.Models.Auth;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Authenticated
{
    public class PaymentAuthApi(IHttpClientFactory factory) : ApiCosmos<AuthSubscription>(factory, ApiType.Authenticated, key: null, [], ApiContext.Default.AuthSubscription)
    {
        public async Task AppleVerify(string receipt, CancellationToken cancellationToken)
        {
            await PostAsync("apple/verify", receipt, ApiContext.Default.String, states: [], cancellationToken);
        }

        public async Task<string?> StripePortalLink(CancellationToken cancellationToken)
        {
            return await GetStringAsync("stripe/portal-link", cancellationToken);
        }
    }
}