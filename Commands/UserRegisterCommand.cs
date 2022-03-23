using aninja_auth_service.Models;
using MediatR;

namespace aninja_auth_service.Commands
{
    public class UserRegisterCommand : IRequest<User?>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
