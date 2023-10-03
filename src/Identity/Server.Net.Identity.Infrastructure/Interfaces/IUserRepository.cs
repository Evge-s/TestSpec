using Server.Net.Common.Specifications;
using Server.Net.Identity.Domain.Models.UserAggregate;

namespace Server.Net.Identity.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);

        Task<IReadOnlyList<User>> GetAllAsync();

        Task<User> AddAsync(User entity);

        Task UpdateAsync(User entity);

        Task DeleteAsync(User entity);

        Task<IReadOnlyList<User>> SearchAsync(Specification<User> specification, bool disableTracking = true, CancellationToken cancellationToken = default);

        Task<int> CountAsync(Specification<User> spec, CancellationToken cancellationToken = default);
    }
}
