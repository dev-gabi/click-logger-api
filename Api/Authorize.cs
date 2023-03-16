using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var userId = context.HttpContext.Items["UserId"];
            var userEmail = context.HttpContext.Items["Email"];
            var loginUserStatsId = context.HttpContext.Items["LoginUserStatsId"];

            if (userId == null || userEmail == null || loginUserStatsId == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
