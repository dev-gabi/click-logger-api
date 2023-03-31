using System.Collections.Generic;

namespace Entities.Response.Activity
{
    public class LoginUserStats_UserResponse :ApiResponse
    {
        public IEnumerable<SessionTimeLowerThanFive> Stats { get; set; }
    }
}
