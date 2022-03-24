using aninja_auth_service.Models;
using aninja_auth_service.Queries;
using aninja_auth_service.Repositories;
using MediatR;

namespace aninja_auth_service.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
    {
        private IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var result = await _userRepository.GetUserById(id);
            return result;
        }
    }
}
