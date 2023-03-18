namespace Entities.Response
{
    public class LoginResponse : ApiResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int LoginUserStatsId { get; set; }
    }
}
