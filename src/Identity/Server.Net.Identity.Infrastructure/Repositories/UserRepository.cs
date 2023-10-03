using Microsoft.EntityFrameworkCore;
using Server.Net.Common.Specifications;
using Server.Net.Identity.Domain.Models.UserAggregate;
using Server.Net.Identity.Infrastructure.DataBase;
using Server.Net.Identity.Infrastructure.Interfaces;

namespace Server.Net.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext userContext;

        public UserRepository(IdentityDbContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<User> AddAsync(User entity)
        {
            await userContext.Users.AddAsync(entity);
            await userContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(User entity)
        {
            userContext.Users.Remove(entity);
            await userContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            userContext.Users.Update(entity);
            await userContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await userContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await userContext.Users.FindAsync(id);
        }

        public async Task<int> CountAsync(Specification<User> spec, CancellationToken cancellationToken = default)
        {
            return await userContext.Users.CountAsync(spec.ToExpression(), cancellationToken);
        }

        public async Task<IReadOnlyList<User>> SearchAsync(Specification<User> specification, bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            var query = userContext.Users
               .Where(specification);

            query = disableTracking ? query.AsNoTracking() : query.AsTracking();

            return await query
                .ToListAsync(cancellationToken);
        }
    }
}
