using Microsoft.EntityFrameworkCore;
using MSA.EventPlatform.DataContext;
using MSA.EventPlatform.Domain.Users;

namespace MSA.EventPlatform.API.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(long userId, CancellationToken cancellationToken, bool track = false);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken, bool track = false);
        Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    }

    public class UserRepository(EventPlatformDbContext dbContext) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(long userId, CancellationToken cancellationToken, bool track = false)
        {
            if (track)
            {
                return await dbContext.Users.FindAsync(userId);
            }

            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken, bool track = false)
        {
            var query = dbContext.Users.AsQueryable();
            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, cancellationToken);
        }

        public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
