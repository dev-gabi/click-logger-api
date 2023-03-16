using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Linq;
using Entities.Configutation;
using Entities;

namespace Api
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _jwtConfig;

        public JWTMiddleware(RequestDelegate next, IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
            _next = next;
        }

        public async Task Invoke(HttpContext  context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                await AttachUserToContext(context, token);

            await  _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.EncKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _jwtConfig.Audience,
                    ValidIssuer = _jwtConfig.Issuer,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string loginUserStatsId = jwtToken.Claims.First(x => x.Type == "LoginUserStatsId").Value;
                string email = jwtToken.Claims.First(x => x.Type == "Email").Value;
                string userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
                // attach user to context on successful jwt validation
                context.Items["LoginUserStatsId"] = loginUserStatsId;
                context.Items["UserId"] = userId;
                context.Items["Email"] = email;

            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes             
            }

        }


    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JWTMiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }
}
