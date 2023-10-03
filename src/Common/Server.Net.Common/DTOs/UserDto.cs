namespace Server.Net.Common.DTOs
{
    public class UserDto : BaseDto
    {
        public string? UserEmail { get; private set; }

        public string? DisplayName { get; private set; }

        public string? FirstName { get; private set; }

        public string? LastName { get; private set; }
    }
}
