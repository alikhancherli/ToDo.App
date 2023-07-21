namespace ToDo.App.Application.JwtService
{
    public class AccessTokenModel
    {
        public DateTime ExpirationTime { get; set; }
        public string? AccessToken { get; set; }
        public string TokenType { get; } = "Bearer";
        public UserClaims UserClaims { get; set; }
    }

    public class UserClaims
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
