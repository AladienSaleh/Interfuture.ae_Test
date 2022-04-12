using interfuture.Data;
using Microsoft.EntityFrameworkCore;

namespace interfuture.Providers
{
    public interface IUserProvider
    {
        Task<int> Create(User user);

        Task<User> Get(int userId);
        Task<(List<User> userList, int totalCount)> GetAll(int offest, int pageSize);

        Task<User> Delete(int userId);

        Task<User> Update(User user);
    }

    public class UserProvider : IUserProvider
    {
        private readonly InterfutureDbContext dbContext;

        public UserProvider(
                InterfutureDbContext dbContext
            )
        {
            this.dbContext = dbContext;
        }
        public async Task<int> Create(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> Delete(int userId)
        {
            var user = await dbContext.Users.FindAsync(userId);
            dbContext.Users.Remove(user);
            return user;
        }

        public async Task<User> Get(int userId)
        {
            var user = await dbContext.Users.FindAsync(userId);
            return user;
        }

        public async Task<(List<User> userList, int totalCount)> GetAll(int offset, int pageSize)
        {
            var userQuery = dbContext.Users
                .AsNoTracking();
            var count = await userQuery.CountAsync();

            List<Data.User>? userList;
            if (pageSize > 0)
            {
                userList = await userQuery.Skip(offset).Take(pageSize).ToListAsync();
            }
            else
            {
                userList = await userQuery.Skip(offset).ToListAsync();
            }
            return (userList, count);
        }

        public async Task<User> Update(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
