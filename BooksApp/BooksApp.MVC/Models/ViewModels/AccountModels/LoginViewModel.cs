using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı adı boş bırakılmamalıdır")]
        public string UserName { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage = "Parola boş bırakılmamalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
