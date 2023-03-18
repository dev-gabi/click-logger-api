using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using static Bl.Enums;

namespace Bl.Auth
{
    public static class AuthHelpers
    {
        public static IEnumerable<Claim> SetUserClaims(User user, int loginUserStatsId)
        {
            return new[]
                {
                    new Claim(AuthContextItems.UserId.ToString(), user.Id.ToString()),
                    new Claim(AuthContextItems.LoginUserStatsId.ToString(),  loginUserStatsId.ToString()),
                    new Claim(AuthContextItems.Email.ToString(), user.Email),
                };
        }

        public static void RemoveJWTContextItems(HttpContext context)
        {
            context.Items.Remove(AuthContextItems.UserId.ToString());
            context.Items.Remove(AuthContextItems.LoginUserStatsId.ToString());
            context.Items.Remove(AuthContextItems.Email.ToString());
        }

        internal static string GetLoginUserStatsIdFromContext(HttpContext context)
        {
            return context.Items[AuthContextItems.LoginUserStatsId.ToString()].ToString();
        }
    }
}
