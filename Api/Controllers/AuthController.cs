using Bl.Auth;
using Entities.Response;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ResponseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Route("/api/test")]
        [HttpGet]
        public string Test()
        {
            return "Test is good";
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([Bind("Email, Password, TimeOnPageInSeconds")]LoginVM vm)
        {

            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(vm);
                if (result != null)
                {
                    return CreateHttpResponse(result);
                }
               return NotFound();
            }

            return CreateHttpResponse(
                new ApiResponse() { Error = ModelState.GetModelStateError()}
                );
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
 
        public async Task<IActionResult> Logout(/*[Bind("LoginUserStatsId")]LogoutVM vm*/)
        {
            var result = await _authService.LogoutAsync(/*vm.LoginUserStatsId,*/ httpContext);
            if (result != null)
            {
                return CreateHttpResponse(result);
            }
            return NotFound();
        }
    }
}
