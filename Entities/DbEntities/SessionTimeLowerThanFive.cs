using System;

namespace Entities
{
    public class SessionTimeLowerThanFive: IGenericEntity, IUserStats
    {
        public int Id { get ; set  ; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string JobTitle { get; set; }
        public int? SessionInMinutes { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get ; set  ; }
    }
}
