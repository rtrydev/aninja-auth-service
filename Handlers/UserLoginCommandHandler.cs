using aninja_auth_service.Commands;
using aninja_auth_service.Helpers;
using aninja_auth_service.Models;
using aninja_auth_service.Repositories;
using MediatR;

namespace aninja_auth_service.Handlers
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, User?>
    {
        private IUserRepository _userRepository;

        public UserLoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByName(request.Name);
            if (user == null) return null;
            var salt = user.PasswordSalt;
            var hashedPassword = Hasher.HashPassword(request.Password, salt);
            var authUser = await _userRepository.GetUserByCredentials(request.Name, hashedPassword);
            return authUser;
        }
    }
}
