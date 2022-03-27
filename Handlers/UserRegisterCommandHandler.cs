using aninja_auth_service.Commands;
using aninja_auth_service.Helpers;
using aninja_auth_service.Models;
using aninja_auth_service.Repositories;
using MediatR;

namespace aninja_auth_service.Handlers
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, User?>
    {
        private IUserRepository _userRepository;
        public UserRegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var hashedPass = Hasher.HashPassword(request.Password);
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email.Trim().ToLower(),
                Password = hashedPass.password,
                PasswordSalt = hashedPass.salt,
                Role = "User"
            };
            if (await _userRepository.Exists(user))
            {
                return null;
            }

            await _userRepository.CreateUser(user);
            return user;
        }
    }
}
