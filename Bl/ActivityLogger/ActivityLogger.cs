using Dal;
using Dal.Enums;
using Entities;
using Entities.DbEntities;
using Entities.Response;
using Entities.UiEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Bl.Enums;

namespace Bl
{
    public class ActivityLogger : IActivityLogger
    {
        internal GenericRepository<LoginPageStats> _pageStatsRepository;
        internal GenericRepository<PageStatsWithUserName> _pageStatsWithNamesView;
        internal GenericRepository<LoginUserStats> _userStatsRepository;
        internal GenericRepository<UserStats_User> _userStatsUserView;
        internal GenericRepository<LoginUserStatsWithUserName> _userStatsWithUserNameFunction;

        public ActivityLogger(GenericRepository<LoginPageStats> pageStatsRepository, GenericRepository<LoginUserStats> userStatsRepository,
            GenericRepository<LoginUserStatsWithUserName> userStatsWithUserNameFunction, GenericRepository<UserStats_User> userStatsUserView, GenericRepository<PageStatsWithUserName> pageStatsWithNamesView)
        {
            _pageStatsRepository = pageStatsRepository;
            _userStatsRepository = userStatsRepository;
            _userStatsUserView = userStatsUserView;
            _pageStatsWithNamesView = pageStatsWithNamesView;
            _userStatsWithUserNameFunction = userStatsWithUserNameFunction;
        }

        public IEnumerable<LoginPageStats> GetLoginPageStats()
        {
            return _pageStatsWithNamesView.GetAll();
        }

        public IEnumerable<LoginUserStats> GetLoginUserStats()
        {
            return _userStatsRepository.GetAll();
        }
        public IEnumerable<UserStats_User> GetLessThanFiveMinutesSessionTime()
        {
            return _userStatsUserView.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="buttonType"></param>
        /// <param name="timeToClickInSeconds"></param>
        /// <returns>LoginUserStatsId</returns>
        public int AddLoginActivity(int userId, int timeToClickInSeconds)
        {
            int loginUserStatsId = AddLoginUserStats(userId);
            AddLoginPageStats(loginUserStatsId, timeToClickInSeconds);
            return loginUserStatsId;
        }

        void AddLoginPageStats(int LoginUserStatsId, int timeToClickInSeconds)
        {
            try
            {
                Dictionary<string, string> columnNamesWithValues = new()
            {
                {nameof(LoginPageStats.LoginUserStatsId) , LoginUserStatsId.ToString() },
                { nameof(LoginPageStats.ButtonType), Buttons.Login.ToString() },
                { nameof(LoginPageStats.ClickedAfterInSeconds), timeToClickInSeconds.ToString() }
            };
                _pageStatsRepository.ExecuteSqlRawSP(StoredProcedures.InsertLoginPageStats.ToString(), columnNamesWithValues);
            }
            catch (Exception)
            {
                throw;
            }

        }

        int AddLoginUserStats(int userId)
        {
            try
            {
                Dictionary<string, string> columnNamesWithValues = new()
            {
                {nameof(LoginUserStats.UserId) , userId.ToString() },
                { nameof(LoginUserStats.LoginTime), DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") }
            };

                return _userStatsRepository.FromSqlRawSP(StoredProcedures.InsertLoginUserStats.ToString(), columnNamesWithValues)
                                             .AsEnumerable().FirstOrDefault().Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateLogoutAsync(string loginUserStatsId)
        {
            Task insertPageStatsForLogout = null;

            LoginUserStats updatedUserStats = await UpdateLogOutInLoginUserStats(loginUserStatsId);
            if (updatedUserStats != null)
            {
                insertPageStatsForLogout = AddLogOutPageStats(loginUserStatsId, updatedUserStats.LoginTime);
                insertPageStatsForLogout.Wait();
            }

            return insertPageStatsForLogout.IsCompletedSuccessfully;
        }

        private Task AddLogOutPageStats(string loginUserStatsId, DateTime loginTime)
        {
            try
            {
                TimeSpan ts = DateTime.Now.Subtract(loginTime);
                int seconds = (int)Math.Ceiling(ts.TotalSeconds);
                Dictionary<string, string> columnNamesWithValues = new()
            {
            { nameof(LoginPageStats.LoginUserStatsId) , loginUserStatsId},
                { nameof(LoginPageStats.ButtonType), Buttons.Logout.ToString() },
                { nameof(LoginPageStats.ClickedAfterInSeconds),   seconds.ToString() }
            };
                _pageStatsRepository.ExecuteSqlRawSP(StoredProcedures.InsertLoginPageStats.ToString(), columnNamesWithValues);
                return Task.CompletedTask;
            }
            catch (Exception x)
            {
                return Task.FromException(x);
            }

        }

        private Task<LoginUserStats> UpdateLogOutInLoginUserStats(string loginUserStatsId)
        {
            try
            {
                LoginUserStats stats = GetLoginUserStatsById(loginUserStatsId);
                Dictionary<string, string> updateLoginUserStatsColomnNamesAndParams = GetLoginUserStatsDictionaryForLogout(stats);

                return Task.FromResult(_userStatsRepository.FromSqlRawSP(StoredProcedures.UpdateLoginUserStats.ToString(), updateLoginUserStatsColomnNamesAndParams).AsEnumerable().FirstOrDefault());
            }
            catch (Exception x)
            {
                return null;
            }


        }

        private LoginUserStats GetLoginUserStatsById(string loginUserStatsId)
        {
            Dictionary<string, string> columnNamesAndParams = new Dictionary<string, string>()
                        {
                            {nameof(LoginUserStats.Id), loginUserStatsId},
                        };
            return _userStatsRepository.GetOneFromSqlRaw(StoredProcedures.SelectLoginUserStatsById.ToString(), columnNamesAndParams);
        }

        private Dictionary<string, string> GetLoginUserStatsDictionaryForLogout(LoginUserStats stats)
        {
            TimeSpan ts = DateTime.Now.Subtract(stats.LoginTime);
            stats.SessionInMinutes = (int)ts.TotalMinutes;

            return new Dictionary<string, string>()
                        {
                            { nameof(LoginUserStats.Id), stats.Id.ToString()},
                            {nameof(LoginUserStats.LogoutTime),  DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")},
                            {nameof(LoginUserStats.SessionInMinutes),  stats.SessionInMinutes.ToString()}
                        };
        }

        public Task<DeleteResponse> DeleteLoginPageStats(int id)
        {
            try
            {
                _pageStatsRepository.Delete(StoredProcedures.DeleteFromLoginPageStats.ToString(), id);
                return CreateDeleteResponse(id);
            }
            catch (Exception x)
            {
                return CreateDeleteResponse(id, $"An error has occured while tring to delete object {id} from LoginPageStats table");

            }
        }

        public Task<DeleteResponse> DeleteLoginUserStats(int id)
        {
            try
            {
                _userStatsRepository.Delete(StoredProcedures.DeleteFromLoginUserStats.ToString(), id);
                return CreateDeleteResponse(id);
            }
            catch (Exception)
            {
                return CreateDeleteResponse(id, $"An error has occured while tring to delete object {id} from LoginUserStats table");
            }

        }

        private Task<DeleteResponse> CreateDeleteResponse(int id, string error = null)
        {
            return Task.FromResult(new DeleteResponse()
            {
                Id = id,
                IsSuccess = true,
                StatusCode = 200,
                Message = $"Object {id} was deleted successfully",
                StatusCodeTitle = "Ok",
                Error = error
            });
        }

        public IEnumerable<LoginUserStatsWithUserName> GetLoginUserStatsByName(string name)
        {
            Dictionary<string, string> columnNamesAndParams = new Dictionary<string, string>()
                        {
                            {nameof(User.UserName), name},
                        };

                return _userStatsWithUserNameFunction.GetManyFromSqlRaw(StoredProcedures.SelectLoginUserStatsByUserName.ToString(), columnNamesAndParams); 
        }
    }
}
