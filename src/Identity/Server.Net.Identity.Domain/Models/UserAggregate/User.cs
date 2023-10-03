using Server.Net.Identity.Domain.Models.Enums;

namespace Server.Net.Identity.Domain.Models.UserAggregate
{
    public class User
    {
        public Guid Id { get; private set; }

        public string? UserEmail { get; private set; }

        public bool IsEmailConfirmed { get; private set; }

        public bool IsBlocked { get; private set; }

        public string? DisplayName { get; private set; }

        public byte[]? PasswordHash { get; private set; }

        public byte[]? PasswordSalt { get; private set; }

        public string? FirstName { get; private set; }

        public string? LastName { get; private set; }

        public byte[]? Image { get; private set; }

        public Role UserRole { get; private set; }

        public ICollection<ExternalProvider> externalProviders { get; private set; } = new List<ExternalProvider>();

        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        private User()
        {
        }

        public User(
            string? userEmail,
            bool isEmailConfirmed,
            string? displayName,
            byte[]? passwordHash,
            byte[]? passwordSalt,
            string? firstName,
            string? lastName,
            byte[]? image)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            IsEmailConfirmed = isEmailConfirmed;
            DisplayName = displayName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            FirstName = firstName;
            LastName = lastName;
            Image = image;
            UserRole = Role.AppUser;

            /*
            if (externalProvider != null)
                externalProviders.Add(externalProvider);

            AddRefreshToken(refreshToken);
            */
        }

        public void ConfirmEmail() // TODO
        {
            IsEmailConfirmed = true;
        }

        public void UpdateUserEmail(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentException("User Email cannot be null or empty.");

            UserEmail = userEmail;
        }

        public void UpdateDisplayName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Display name cannot be null or empty.");

            DisplayName = newName;
        }

        public void UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First Name cannot be null or empty.");

            FirstName = firstName;
        }

        public void UpdateUserPassword(byte[] passwordHash, byte[] salt)
        {
            if (passwordHash == null || passwordHash.Length == 0)
                throw new ArgumentException("Password hash cannot be null or empty.");

            if (salt == null || salt.Length == 0)
                throw new ArgumentException("Salt cannot be null or empty.");

            PasswordHash = passwordHash;
            PasswordSalt = salt;
        }

        public void UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last Name cannot be null or empty.");

            LastName = lastName;
        }

        public void UpdateImage(byte[] image)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentException("Image cannot be null or empty.");

            Image = image;
        }

        // ExternalProvider
        public void AddExternalProvider(ExternalProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            externalProviders.Add(provider);
        }

        public void RemoveExternalProvider(ExternalProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            externalProviders.Remove(provider);
        }

        public void ClearExternalProviders()
        {
            externalProviders.Clear();
        }

        // RefreshToken
        public void AddRefreshToken(RefreshToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            RefreshTokens.Add(token);
        }

        public void RemoveRefreshToken(RefreshToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            RefreshTokens.Remove(token);
        }

        public void ClearRefreshTokens()
        {
            RefreshTokens.Clear();
        }
    }
}
