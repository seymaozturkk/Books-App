using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class RegisterViewModel
    {
        [DisplayName("Ad")]
        [Required(ErrorMessage="Ad alanı zorunludur")]
        public string FirstName { get; set; }

        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        public string LastName { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur")]
        public string UserName { get; set; }

        [DisplayName("Eposta")]
        [Required(ErrorMessage = "Eposta alanı zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage = "Parola alanı zorunludur")]
        [DataType(DataType. Password)]
        public string Password { get; set; }

        [DisplayName("Parola Tekrar")]
        [Required(ErrorMessage = "Parola tekrar alanı zorunludur")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="İki parola aynı olmalıdır!")]
        public string RePassword { get; set; }
    }
}
