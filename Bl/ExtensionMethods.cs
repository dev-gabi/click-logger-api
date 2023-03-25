using Entities;
using Entities.Response;
using Entities.Response.Activity;
using Entities.UiEntities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Bl
{
    public static class ExtensionMethods
    {
        public static LoginResponse ConvertToLoginResponse(this User user, System.IdentityModel.Tokens.Jwt.JwtSecurityToken token)
        {
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse()
            {
                IsSuccess = true,
                Message = $"{user.UserName} Logged In Successfully",
                StatusCode = 200,
                StatusCodeTitle = "Ok",
                UserId = user.Id,
                UserName = user.UserName,
                Token = tokenAsString

            };
        }
        public static LoginPageStatsResponse ConvertToLoginPageStatsResponse(this IEnumerable<LoginPageStats> stats)
        {
            return new LoginPageStatsResponse()
            {
                 IsSuccess = true,
                 Stats = stats,
                 StatusCode = 200,
                 StatusCodeTitle = "Ok",
                 Message = "fetch success"
            };
        }

        public static LoginUserStats_UserResponse ConvertToUserStats_UserResponse(this IEnumerable<UserStats_User> stats)
        {
            return new LoginUserStats_UserResponse()
            {
                IsSuccess = true,
                Stats = stats,
                StatusCode = 200,
                StatusCodeTitle = "Ok",
                Message = "fetch success"
            };
        }

        public static LoginPageStatsResponse ConvertToLoginPagestatsResponse(this IEnumerable<LoginPageStats> stats)
        {
            return new LoginPageStatsResponse()
            {
                IsSuccess = true,
                Stats = stats,
                StatusCode = 200,
                StatusCodeTitle = "Ok"
            };
        }

        public static LoginUserStatsResponse ConvertToLoginUserstatsResponse(this IEnumerable<LoginUserStatsWithUserName> stats)
        {
            return new LoginUserStatsResponse()
            {
                IsSuccess = true,
                Stats = stats,
                StatusCode = 200,
                StatusCodeTitle = "Ok"
            };
        }

    }
}
