using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.UiEntities
{

    
    public class LoginUserStatsWithUserName : LoginUserStats, IGenericEntity//, IUserStats
    {
        public string UserName{ get; set; }
//        public int Id { get; set; }
//        public int UserId { get; set; }
//        public DateTime LoginTime { get; set; }
//#nullable enable
//        public DateTime? LogoutTime { get; set; }

//        public int? SessionInMinutes { get; set; }
#nullable disable

    }
}
