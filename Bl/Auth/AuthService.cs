using Dal;
using Dal.Enums;
using Entities;
using Entities.Configutation;
using Entities.Response;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Auth
{
    public class AuthService : IAuthService
    {
        private  IActivityLogger _logger;

        GenericRepository<User> _usersRepository;
        private readonly JwtConfig _jwtConfig;


        public AuthService(IActivityLogger logger,  GenericRepository<User> usersRepository, IOptions<JwtConfig> jwtConfig)
        {
            _logger = logger;
            _jwtConfig = jwtConfig.Value;
            _usersRepository = usersRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginVM vm)
        {
            try
            {
                User user = await GetUserFromDB(vm);
                if (user != null)
                {
                    int loginUserStatsId = _logger.AddLoginActivity(user.Id,  vm.TimeOnPageInSeconds);

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.EncKey));
                    var token = new JwtSecurityToken(
                                issuer: _jwtConfig.Issuer,
                                audience: _jwtConfig.Audience,
                                claims: AuthHelpers.SetUserClaims(user, loginUserStatsId),
                                expires: DateTime.Now.AddHours(1),
                                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                    return user.ConvertToLoginResponse(token);
                }
            }
            catch (Exception x)
            {
                throw;
            }
            return null;
        }

        private Task<User> GetUserFromDB(LoginVM vm)
        {
            Dictionary<string, string> columnNamesAndParams = new Dictionary<string, string>()
            {
                {nameof(LoginVM.Email), vm.Email},
                {nameof(LoginVM.Password), vm.Password }
            };
            return Task.FromResult(
                _usersRepository.GetOneFromSqlRaw(StoredProcedures.SelectUserByEmailAndPassword.ToString(), columnNamesAndParams)
                );
        }

        public async Task<LogoutResponse> LogoutAsync(/*int loginPageStatsId, */HttpContext httpContext)
        {
            try
            {
                string loginUserStatsId = AuthHelpers.GetLoginUserStatsIdFromContext(httpContext);
                AuthHelpers.RemoveJWTContextItems(httpContext);
                bool isLogged = await _logger.UpdateLogoutAsync(loginUserStatsId);
                if (isLogged)
                {
                    return new LogoutResponse() { IsSuccess = true, StatusCode = 200, Message = "User loged out", StatusCodeTitle = "Ok" };
                }
            }
            catch (Exception x)
            {
                return new LogoutResponse() { IsSuccess = false, StatusCode = 500, Message = "Error while tring to logout", StatusCodeTitle = "Internal Server Error" };
            }
            return null;
        }

    }
}
