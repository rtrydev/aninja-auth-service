using aninja_auth_service.Helpers;
using aninja_auth_service.Models;
using MongoDB.Driver;

namespace aninja_auth_service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoClient _mongoClient;
        private IMongoCollection<User> _users;

        public UserRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _users = _mongoClient.GetDatabase("usersDB").GetCollection<User>("users");
        }
        public async Task CreateUser(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<bool> Exists(User user)
        {
            var result = await _users.FindAsync(x => x.Name == user.Name || x.Email == user.Email);
            return result.Any();
        }

        public async Task<User?> GetUserByCredentials(string username, string password)
        {
            var user = await (await _users.FindAsync<User>(x => x.Name == username)).FirstOrDefaultAsync();
            if (user == null) return null;
            var userPassword = user.Password;
            var passwordSalt = user.PasswordSalt;
            var hashedPass = Hasher.HashPassword(password, passwordSalt);
            if(hashedPass == userPassword) return user;
            return null;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            var result = await _users.FindAsync<User>(x => x.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<User?> UpdateUser(User user)
        {
            var result = await _users.FindOneAndReplaceAsync<User>(x => x.Id == user.Id, user);
            return result;
        }
    }
}
