using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public abstract class ResponseController : Controller
    {
        internal HttpContext httpContext;

        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            httpContext = ctx.HttpContext;
        }

            protected IActionResult CreateHttpResponse<T>(T response)
        {   
            string error = (string)response.GetType().GetProperty("Error").GetValue(response);

            if (string.IsNullOrEmpty(error))
            {
                return Ok(response);
            }
            return Problem(detail: error);
        }


    }
}
