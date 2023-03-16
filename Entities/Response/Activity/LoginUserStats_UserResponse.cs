using System.Collections.Generic;

namespace Entities.Response.Activity
{
    public class LoginUserStats_UserResponse :ApiResponse
    {
        public IEnumerable<UserStats_User> Stats { get; set; }
    }
}
