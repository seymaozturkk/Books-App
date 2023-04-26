using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels.Accounts
{
    public class RoleAddViewModel
    {
        [DisplayName("Rol Adı")]
        [Required(ErrorMessage ="Rol adı zorunludur")]
        public string Name { get; set; }

        [DisplayName("Rol Açıklaması")]
        [Required(ErrorMessage = "Rol açıklaması zorunludur")]
        public string Description { get; set; }
    }
}
