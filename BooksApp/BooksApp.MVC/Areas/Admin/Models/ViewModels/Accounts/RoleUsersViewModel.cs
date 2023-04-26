using BooksApp.Entity.Concrete.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels.Accounts
{
    public class RoleUsersViewModel
    {
        public RoleUsersViewModel()
        {
            RoleUpdateViewModel = new RoleUpdateViewModel();
        }
        public List<SelectListItem> SelectRoleList { get; set; }
        public List<User> Users { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public RoleUpdateViewModel RoleUpdateViewModel { get; set; }
        public Role Role { get; set; }
    }
}
