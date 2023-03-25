using System;

namespace Entities
{
    public interface IUserStats
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
#nullable enable
        public DateTime? LogoutTime { get; set; }

        public int? SessionInMinutes { get; set; }
#nullable disable
    }

    //if LoginUserStatsWithUserName is derived from LoginUserStats 
    //and exception will be thrown when try to use SelectLoginUserStatsByUserName procedure
    //derived entity can't directly be mapped to a function - see AppContext line 25
    // tha't is why both LoginUserStats and LoginUserStatsWithUserName implements IUserStats instead of inheritance
}
