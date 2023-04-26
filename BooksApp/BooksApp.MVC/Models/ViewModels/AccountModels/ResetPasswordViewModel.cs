using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; }

        [DisplayName("Eposta Adresi")]
        [Required(ErrorMessage ="Email adresi zorunludur!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage ="Parola zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Parola Tekrar")]
        [Required(ErrorMessage = "Parola tekrar zorunludur")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Parolalar aynı olmalıdır!")]
        public string RePassword { get; set; }

    }
}
