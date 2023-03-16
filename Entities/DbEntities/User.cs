using System;

namespace Entities
{
    public class User :IGenericEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
#nullable enable
        public string? JobTitle { get; set; }
        public int? PhoneNumber { get; set; }
#nullable disable
    }
}
