using Ardalis.SmartEnum;

namespace Server.Net.Identity.Domain.Models.Enums
{
    public sealed class ExternalProviderService : SmartEnum<ExternalProviderService>
    {
        public static readonly ExternalProviderService Google = new(nameof(Google), 1);

        public static readonly ExternalProviderService Facebook = new(nameof(Facebook), 2);

        public ExternalProviderService(string name, int value) : base(name, value)
        {
        }
    }
}
