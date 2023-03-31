using Bl;
using Entities;
using Entities.Response;
using Entities.UiEntities;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginUserStatsController : ResponseController
    {
        private readonly IActivityLogger _activityLogger;
        public LoginUserStatsController(IActivityLogger activityLogger)
        {
            _activityLogger = activityLogger;
        }


        [HttpGet]
        public IActionResult Get()
        {  
             var result = _activityLogger.GetLoginUserStats();
            if (result != null)
            {
                return CreateHttpResponse(result.ConvertToLoginUserStatsResponse());
            }
            return CreateHttpResponse(new ApiResponse() { Error = "Some thing went wrong" });
        }


        [HttpPost("Name")]
        public IActionResult GetByName([Bind("Name")] UserStatsByNameVM vm)
        {
            var result = _activityLogger.GetLoginUserStatsByName(vm.Name);
            if (result != null)
            {
              
                return CreateHttpResponse(result.ConvertToLoginUserStatsResponse());
            }
            return null;
        }

        [HttpGet("SessionsLowerThanFive")]
        public IActionResult GetSessionsLowerThanFive()
        {
            var result = _activityLogger.GetSessionTimeLowerThanFive();
            if (result != null)
            {
                return CreateHttpResponse(result.ConvertToUserStats_UserResponse());
            }

            return CreateHttpResponse(new ApiResponse() { Error = "Some thing went wrong" });
        }

        [HttpDelete]
        public IActionResult Delete([Bind("Id")]DeleteVM vm)
        {
            var result = _activityLogger.DeleteLoginUserStats(vm.Id).Result;

            return CreateHttpResponse(result);
        }



    }
}
