using Bl;
using Entities.Response;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {  
            var result = _activityLogger.GetLoginUserStats();
            if (result != null)
            {
                return CreateHttpResponse(result.ConvertToLoginUserstatsResponse());
            }

            return CreateHttpResponse( new ApiResponse() { Error = "Some thing went wrong" }  );
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
