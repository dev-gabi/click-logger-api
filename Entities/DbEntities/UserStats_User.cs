using System;

namespace Entities
{
    public class UserStats_User: IGenericEntity
    {
        public string UserName { get; set; }
        public string JobTitle { get; set; }
        public int SessionInMinutes { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
