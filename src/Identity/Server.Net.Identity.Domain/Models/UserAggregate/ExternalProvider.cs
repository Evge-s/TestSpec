using Server.Net.Identity.Domain.Models.Enums;

namespace Server.Net.Identity.Domain.Models.UserAggregate
{
    public class ExternalProvider
    {
        public ExternalProviderService externalProviderService { get; private set; }

        public string ProviderKey { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        private ExternalProvider()
        {
        }

        public ExternalProvider(ExternalProviderService externalProviderService, string providerKey, User user)
        {
            this.externalProviderService = externalProviderService;
            ProviderKey = providerKey;
            UserId = user.Id;
            User = user;
        }
    }
}
