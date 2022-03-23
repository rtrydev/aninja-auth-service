using aninja_auth_service.Models;

namespace aninja_auth_service.Authorization
{
    public interface IJwtService
    {
        public string? GetJwtToken(User user);

    }
}
