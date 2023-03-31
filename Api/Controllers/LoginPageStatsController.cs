using Bl;
using Entities;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

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
        public IEnumerable<LoginPageStats> GetLoginPageStats()
        {
            return _activityLogger.GetLoginPageStats();
        }

        [HttpDelete]
        public IActionResult DeleteLoginPageStats([Bind("Id")] DeleteVM vm)
        { //todo: test in client
            var result = _activityLogger.DeleteLoginUserStats(vm.Id).Result;

            return CreateHttpResponse(result);
        }
    }
}
