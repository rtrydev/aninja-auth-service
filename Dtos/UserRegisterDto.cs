using System.ComponentModel.DataAnnotations;

namespace aninja_auth_service.Dtos
{
    public class UserRegisterDto
    {
        [MaxLength(24)]
        [MinLength(3)]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\.\-_]{1,}@[a-zA-Z0-9\-_]{1,}\.[a-zA-Z\.]{1,}$")]
        [DefaultValue("a@a.com")]
        public string Email { get; set; }
        [MaxLength(24)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
