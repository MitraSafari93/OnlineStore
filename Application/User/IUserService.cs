

using OnlineStore.Domain;

namespace Application
{
    public interface IUserService
    {
        public Task CreateUser(string name);
        public Task<User> GetUserById(int userId);
    }
}