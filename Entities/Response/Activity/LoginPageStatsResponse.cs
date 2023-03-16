using System.Collections.Generic;

namespace Entities.Response.Activity
{
    public class LoginPageStatsResponse :ApiResponse
    {
        public IEnumerable<LoginPageStats> Stats { get; set; }
    }
}
