using System;

namespace Entities.UiEntities
{


    public abstract class LoginUserStatsWithUserName : IUserStats, IGenericEntity
    {
        public string UserName{ get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
#nullable enable
        public DateTime? LogoutTime { get; set; }

        public int? SessionInMinutes { get; set; }
#nullable disable

        public virtual User User { get; set; }

#nullable disable

    }
}
