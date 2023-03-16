using Bl;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginPageStatsController :  ResponseController
    {
        private readonly IActivityLogger _activityLogger;
        public LoginPageStatsController(IActivityLogger activityLogger)
        {
            _activityLogger = activityLogger;
        }

        [HttpGet]
        public IActionResult GetLoginPageStats()
        {
            var result = _activityLogger.GetLoginPageStats();
            if (result != null)
            {

                return CreateHttpResponse(result.ConvertToLoginPageStatsResponse());
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteLoginPageStats([Bind("Id")] DeleteVM vm)
        {
            var result = _activityLogger.DeleteLoginUserStats(vm.Id).Result;

            return CreateHttpResponse(result);
        }
    }
}
