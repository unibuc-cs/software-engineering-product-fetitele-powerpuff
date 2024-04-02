using System.ComponentModel.DataAnnotations;

namespace healthy_lifestyle_web_app.Models
{
    public class RegisterModel
    {
        [EmailAddress(ErrorMessage = "Adresa de email invalida")]
        [Required(ErrorMessage = "Emailul este obligatoriu")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Parolele nu sunt identice")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
