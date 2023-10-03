namespace Server.Net.Identity.Domain.Models.UserAggregate
{
    public class RefreshToken
    {
        public string Token { get; private set; }
        public DateTime CreatedDateUtc { get; private set; }
        public DateTime ExpiryDateUtc { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        private RefreshToken()
        {
        }

        public RefreshToken(string token, DateTime createdDateUtc, DateTime expiryDateUtc, User user)
        {
            Token = token;
            CreatedDateUtc = createdDateUtc;
            ExpiryDateUtc = expiryDateUtc;
            UserId = user.Id;
            User = user;
        }

        public bool IsValid() => DateTime.UtcNow > ExpiryDateUtc;
    }
}
