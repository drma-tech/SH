using SH.Shared.Models.Auth;

namespace SH.WEB.Modules.Auth.Core
{
    public class SuperData
    {
        public AuthPrincipal? Principal { get; set; }
        public AuthLogin? Login { get; set; }
    }
}