using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels.AccountModels
{
    public class UserManageViewModel
    {
        public string Id { get; set; }

        [DisplayName("Adı")]
        [Required(ErrorMessage ="Ad alanı boş bırakılamaz")]
        public string FirstName { get; set; }

        [DisplayName("Soyadı")]
        [Required(ErrorMessage = "Soyadı alanı boş bırakılamaz")]
        public string LastName { get; set; }

        [DisplayName("Cinsiyet")]
        public string Gender { get; set; }

        [DisplayName("Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Adres")]
        public string Address { get; set; }

        [DisplayName("Şehir")]
        public string City { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı adı boş bırakılamaz")]
        public string UserName { get; set; }

        [DisplayName("Eposta")]
        [Required(ErrorMessage = "Eposta adresi boş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public List<SelectListItem> GenderSelectList { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}
