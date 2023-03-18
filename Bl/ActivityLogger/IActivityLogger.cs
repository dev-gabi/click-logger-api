using Entities;
using Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bl
{
    public interface IActivityLogger
    {
        public IEnumerable<LoginPageStats> GetLoginPageStats();
        public int AddLoginActivity(int userId,  int timeToClickInSeconds);
        public Task<bool> UpdateLogoutAsync(string loginUserStatsId);
        public IEnumerable<UserStats_User> GetLessThanFiveMinutesSessionTime();
        public Task<DeleteResponse> DeleteLoginUserStats(int id);
        public Task<DeleteResponse>  DeleteLoginPageStats(int id);
        public IEnumerable<LoginUserStats> GetLoginUserStats();
    }
}
