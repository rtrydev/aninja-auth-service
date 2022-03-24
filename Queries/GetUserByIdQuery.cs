using aninja_auth_service.Models;
using MediatR;

namespace aninja_auth_service.Queries
{
    public class GetUserByIdQuery : IRequest<User?>
    {
        public Guid Id { get; set; }
    }
}
