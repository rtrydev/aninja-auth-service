using aninja_auth_service.Models;

namespace aninja_auth_service.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByCreditionals(string username, string password);
        public Task CreateUser(User user);
        public Task<User?> GetUserById(Guid id);
        public Task<User?> UpdateUser(User user);
    }
}
