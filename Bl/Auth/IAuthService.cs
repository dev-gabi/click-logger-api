using Entities.Response;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Bl.Auth
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginVM vm);
        public Task<LogoutResponse> LogoutAsync(/*int loginPageStatsId,*/ HttpContext context);
    }
}
