using SH.Shared.Models.Auth;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Admin
{
    public class PrincipalAdminApi(IHttpClientFactory factory) : ApiCosmos<AuthPrincipal>(factory, ApiType.Anonymous, "principal_import", [], ApiContext.Default.AuthPrincipal)
    {
        public async Task<IEnumerable<AuthPrincipal>> GetAll(CancellationToken cancellationToken)
        {
            return await GetListAsync("principal/get-all", states: [], cancellationToken);
        }

        public async Task Migrate(string? oldId, string? newId, CancellationToken cancellationToken)
        {
            await PostAsync($"principal/migrate/{oldId}/{newId}", null, ApiContext.Default.AuthPrincipal, states: [], cancellationToken);
        }
    }
}