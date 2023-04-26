using BooksApp.Entity.Concrete.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels.Accounts
{
    public class UserUpdateViewModel
    {
        public string Id { get; set; }

        [DisplayName("Ad")]
        [Required(ErrorMessage ="Ad zorunludur")]
        public string FirstName { get; set; }

        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Soyad zorunludur")]
        public string LastName { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Email Onayı")]
        public bool EmailConfirmed { get; set; }

        [Required(ErrorMessage ="En az bir rol seçilmelidir")]
        public IList<string> SelectedRoles { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
