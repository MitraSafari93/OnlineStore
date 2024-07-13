using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Domain;
using OnlineStore.Infrustructure;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUser(string name)
        {
            var user = new User
            {
                Name = name,
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }
    }


}