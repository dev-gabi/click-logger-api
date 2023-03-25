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


        [HttpGet("GetAll")]
        public IEnumerable<LoginUserStats> GetAll()
        {  //todo: return LoginUserStatsResponse
            return _activityLogger.GetLoginUserStats();

        }

        [HttpPost("Name")]
        public IEnumerable<LoginUserStatsWithUserName> GetByName([Bind("Name")] UserStatsByNameVM vm)
        {//todo: return LoginUserStatsResponse
            if (ModelState.IsValid)
            {
                return _activityLogger.GetLoginUserStatsByName(vm.Name);
            }
            return null;
          }

        [HttpGet("LessThanFiveMinutesSessions")]
        public IActionResult GetLessThanFiveMinutesSessions()
        {   //gets a view from database
            var result = _activityLogger.GetLessThanFiveMinutesSessionTime();
            if (result != null)
            {
                return CreateHttpResponse(result.ConvertToUserStats_UserResponse());
            }

            return CreateHttpResponse(   new ApiResponse() { Error = "Some thing went wrong" }      );
        }

        [HttpDelete]
        public IActionResult DeleteLoginUserStats([Bind("Id")]DeleteVM vm) {
            var result = _activityLogger.DeleteLoginUserStats(vm.Id).Result;

            return CreateHttpResponse(result);
        }



    }
}
