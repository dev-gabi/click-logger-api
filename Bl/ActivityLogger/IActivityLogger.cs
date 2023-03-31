using Entities;
using Entities.Response;
using Entities.UiEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bl
{
    public interface IActivityLogger
    {
        public IEnumerable<LoginPageStats> GetLoginPageStats();
        public int AddLoginActivity(int userId,  int timeToClickInSeconds);
        public Task<bool> UpdateLogoutAsync(string loginUserStatsId);
        public IEnumerable<SessionTimeLowerThanFive> GetSessionTimeLowerThanFive();
        public Task<DeleteResponse> DeleteLoginUserStats(int id);
        public Task<DeleteResponse>  DeleteLoginPageStats(int id);
        public IEnumerable<LoginUserStatsWithUserNameView> GetLoginUserStats();
        public IEnumerable<LoginUserStatsWithUserNameSP> GetLoginUserStatsByName(string name);
    }
}
