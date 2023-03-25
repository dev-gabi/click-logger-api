using System;

namespace Entities
{
    public class LoginUserStats: IGenericEntity, IUserStats
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
#nullable enable
        public DateTime? LogoutTime { get; set; }

        public int? SessionInMinutes { get; set; }
#nullable disable
        
        public virtual User User { get; set; }
    }
}
