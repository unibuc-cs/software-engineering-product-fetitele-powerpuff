using System.ComponentModel.DataAnnotations;

namespace healthy_lifestyle_web_app.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "This field must not be empty")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "This field must not be empty")]
        public string? Password { get; set; }
    }
}
