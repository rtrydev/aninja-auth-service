using System.ComponentModel.DataAnnotations;

namespace aninja_auth_service.Dtos
{
    public class UserLoginDto
    {
        [MaxLength(24)]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(24)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
