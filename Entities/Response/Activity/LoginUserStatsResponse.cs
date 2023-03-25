using Entities.UiEntities;
using System.Collections.Generic;

namespace Entities.Response.Activity
{
    public class LoginUserStatsResponse:ApiResponse
    {
        public IEnumerable<LoginUserStatsWithUserName> Stats { get; set; }
    }
}
