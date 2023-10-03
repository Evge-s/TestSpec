using Ardalis.SmartEnum;

namespace Server.Net.Identity.Domain.Models.Enums
{
    public sealed class Role : SmartEnum<Role>
    {
        public static readonly Role AppUser = new(nameof(AppUser), 1);

        public static readonly Role Moderator = new(nameof(Moderator), 2);

        public static readonly Role Administrator = new(nameof(Administrator), 3);

        public Role(string name, int value) : base(name, value)
        {                
        }
    }
}
