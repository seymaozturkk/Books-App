using BooksApp.Core;
using BooksApp.Entity.Concrete.Identity;
using BooksApp.MVC.Areas.Admin.Models.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BooksApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<RoleViewModel> roles = await _roleManager.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToListAsync();

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleAddViewModel roleAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new Role
                {
                    Name = roleAddViewModel.Name,
                    Description = roleAddViewModel.Description
                });
                if (result.Succeeded)
                {
                    TempData["Message"] = Jobs.CreateMessage(
                        "Başarılı!",
                        "Rol bilgisi başarıyla oluşturuldu.",
                        "success");
                    //Bu tip mesajlar için kullanılan toastr adlı js kütüphanesini araştırın. Kullanmaya çalışın. 
                }
                return RedirectToAction("Index", "Roles");
            }

            return View(roleAddViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            var users = _userManager.Users.ToList();
            var members = new List<User>();//İlgili role ait olan üyeler
            var nonMembers = new List<User>();//İlgili role ait olmayan üyeler
            foreach (var user in users)
            {
                #region Uzun Yol
                //bool isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                //if (isInRole)
                //{
                //    members.Add(user);
                //}
                //else
                //{
                //    nonMembers.Add(user);
                //}
                #endregion
                #region Tercih Edeceğimiz Yol
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
                #endregion
            }
            RoleUpdateViewModel roleUpdateViewModel = new RoleUpdateViewModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(roleUpdateViewModel);
        }

        public async Task InitialUsers(RoleUpdateViewModel roleUpdateViewModel)
        {
            foreach (var userId in roleUpdateViewModel.IdsToAdd ?? new string[] { })
            {
                User user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.AddToRoleAsync(user, roleUpdateViewModel.Role.Name);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            foreach (var userId in roleUpdateViewModel.IdsToRemove ?? new string[] { })
            {
                User user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.RemoveFromRoleAsync(user, roleUpdateViewModel.Role.Name);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpdateViewModel roleUpdateViewModel)
        {
            await InitialUsers(roleUpdateViewModel);
            return Redirect("/Admin/Roles/Edit/" + roleUpdateViewModel.Role.Id);
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssignment(RoleUsersViewModel roleUsersViewModel)
        {
            string activeRoleId = "";
            var roles = await _roleManager.Roles.ToListAsync();
            if (roleUsersViewModel.RoleId == null)
            {
                activeRoleId = roles.FirstOrDefault().Id;
            }
            else
            {
                activeRoleId= roleUsersViewModel.RoleId;

            }
            
            List<SelectListItem> selectRoleList = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id,
                Selected = r.Id == activeRoleId ? true : false
            }).ToList();

            var role = await _roleManager.FindByIdAsync(activeRoleId);
            var members = new List<User>();
            var nonMembers = new List<User>();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            };

            roleUsersViewModel.SelectRoleList = selectRoleList;
            roleUsersViewModel.RoleUpdateViewModel.Members = members;
            roleUsersViewModel.RoleUpdateViewModel.NonMembers = nonMembers;
            roleUsersViewModel.Role = role;

            return View(roleUsersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssignment(RoleUpdateViewModel roleUpdateViewModel)
        {
            await InitialUsers(roleUpdateViewModel);
            var roles = await _roleManager.Roles.ToListAsync();
            List<SelectListItem> selectRoleList = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id,
                Selected = r.Id == roleUpdateViewModel.Role.Id ? true : false
            }).ToList();
            var role = await _roleManager.FindByIdAsync(roleUpdateViewModel.Role.Id);
            var members = new List<User>();
            var nonMembers = new List<User>();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            };
            RoleUsersViewModel roleUsersViewModel = new RoleUsersViewModel();
            roleUsersViewModel.SelectRoleList = selectRoleList;
            roleUsersViewModel.RoleUpdateViewModel.Members = members;
            roleUsersViewModel.RoleUpdateViewModel.NonMembers = nonMembers;
            roleUsersViewModel.Role = role;
            return View("RoleAssignment", roleUsersViewModel);
        }
    }
}
