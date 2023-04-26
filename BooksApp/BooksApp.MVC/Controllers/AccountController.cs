using BooksApp.Business.Abstract;
using BooksApp.Core;
using BooksApp.Entity.Concrete.Identity;
using BooksApp.MVC.EmailServices;
using BooksApp.MVC.Models.ViewModels;
using BooksApp.MVC.Models.ViewModels.AccountModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksApp.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ICartService cartService, IOrderService orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cartService = cartService;
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    NormalizedName = (registerViewModel.FirstName + registerViewModel.LastName).ToUpper()
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    //Her yeni kaydolan kullanıcı User rolünde olacak.
                    await _userManager.AddToRoleAsync(user, "User");
                    //Her yeni kaydolan kullanıcının Cart'ı oluşturulacak.
                    await _cartService.InitializeCart(user.Id);
                    TempData["Message"] = Jobs.CreateMessage("Kayıt İşlemi", "Kaydınız başarıyla oluşturulmuştur.", "success");
                    return RedirectToAction("Login", "Account");
                }
            }
            TempData["Message"] = Jobs.CreateMessage("HATA", "Bir hata oluştu, tekrar deneyiniz", "danger");
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            return View(new LoginViewModel { ReturnUrl=returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı bilgileri hatalı!");
                    return View(loginViewModel);
                }
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, isPersistent: true, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return Redirect(loginViewModel.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "Kullanıcı adı ya da parola hatalı!");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string id)
        {
            string name = id;
            if (String.IsNullOrEmpty(name))
            {
                return NotFound();
            }
            User user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            List<SelectListItem> genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem
            {
                Text = "Kadın",
                Value = "Kadın",
                Selected = user.Gender == "Kadın" ? true : false
            });
            genderList.Add(new SelectListItem
            {
                Text = "Erkek",
                Value = "Erkek",
                Selected = user.Gender == "Erkek" ? true : false
            });
            UserManageViewModel userManageViewModel = new UserManageViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                UserName = user.UserName,
                Address = user.Address,
                City = user.City,
                Email = user.Email,
                GenderSelectList = genderList
            };
            var orderList = await _orderService.GetAllOrdersAsync(user.Id);
            List<OrderViewModel> orders = orderList.Select(o => new OrderViewModel
            {
                 Id=o.Id,
                 FirstName = o.FirstName,
                 LastName = o.LastName,
                 City=o.City,
                 Address=o.Address, 
                 Email=o.Email,
                 Phone=o.Phone,
                 OrderDate=o.OrderDate,
                 OrderItems=o.OrderItems.Select(oi=>new CartItemViewModel
                 {
                     CartItemId=oi.Id,
                     BookName=oi.Book.Name,
                     ItemPrice=oi.Price,
                     Quantity=oi.Quantity,
                     ImageUrl = oi.Book.Images[0].Url,
                     BookUrl=oi.Book.Url
                 }).ToList()
            }).ToList();
            userManageViewModel.Orders = orders;
            return View(userManageViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(UserManageViewModel userManageViewModel)
        {
            if (userManageViewModel == null) { return NotFound(); }
            User user = await _userManager.FindByIdAsync(userManageViewModel.Id);
            bool logOut = !(user.UserName == userManageViewModel.UserName);
            user.FirstName = userManageViewModel.FirstName;
            user.LastName = userManageViewModel.LastName;   
            user.Gender = userManageViewModel.Gender;
            user.UserName = userManageViewModel.UserName;
            user.Address = userManageViewModel.Address;
            user.City = userManageViewModel.City;
            user.Email = userManageViewModel.Email;
            user.DateOfBirth = userManageViewModel.DateOfBirth;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (logOut)
                {
                    TempData["Message"] = Jobs.CreateMessage("Başarılı", "Profiliniz başarıyla güncellenmiştir. Kullanıcı adınız değiştiği için yeniden giriş yapmalısınız!", "warning");
                    return RedirectToAction("Logout");
                }
                TempData["Message"] = Jobs.CreateMessage("Başarılı", "Profiliniz başarıyla güncellenmiştir.", "success");
                return Redirect("/Account/Manage/" + user.UserName);
            }
            List<SelectListItem> genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem
            {
                Text = "Kadın",
                Value = "Kadın",
                Selected = user.Gender == "Kadın" ? true : false
            });
            genderList.Add(new SelectListItem
            {
                Text = "Erkek",
                Value = "Erkek",
                Selected = user.Gender == "Erkek" ? true : false
            });
            userManageViewModel.GenderSelectList = genderList;
            return View(userManageViewModel);
            //Eğer kullanıcı username değişikliği yaparsa menüdeki username bilgisi değişmiyor, bunu araştırın!

        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId==null || token == null)
            {
                return View();
            }
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //Eğer email onayını kullanıyorsak role atama ve cart oluşturma burada yapılır.
                    TempData["Message"] = Jobs.CreateMessage("Başarılı", "Profiliniz başarıyla onaylanmıştır. Güvenli alışverişler!", "success");
                    return View();
                }
            }
            TempData["Message"] = Jobs.CreateMessage("HATA", "Profiliniz onaylanırken bir hata oluştu, detaylı bilgi için 0212 645 25 25'i arayınız.", "danger");
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                TempData["Message"] = Jobs.CreateMessage("HATA!", "Lütfen mail adresinizi yazınızı!", "warning");
                return View();
            }
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null) {
                TempData["Message"] = Jobs.CreateMessage("HATA!", "Böyle bir kullanıcı bulunamadı! Lütfen kontrol ederek yeniden deneyiniz!", "warning");
                return View();
            }
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            await _emailSender.SendEmailAsync(
                email,
                "BooksApp Şifre Sıfırlama!",
                $"Parolanızı yeniden belirlemek için <a href='http://localhost:5265{url}'>tıklayınız</a>"
                );
            TempData["Message"] = Jobs.CreateMessage(
                "BİLGİLENDİRME!",
                "Lütfen mail adresinize gelen maili kontrol edip, yönergeleri takip ederek parolanızı yeniden belirlemeyi deneyiniz!",
                "warning");
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            if(userId==null || token == null)
            {
                TempData["Message"] = Jobs.CreateMessage(
                    "Geçersiz İşlem!",
                    "Beklenmedik bir hata oluştu, lütfen defolun!",
                    "warning");
                return Redirect("/");
            }
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel
            {
                Token = token
            };
            return View(resetPasswordViewModel);
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid) { return View(resetPasswordViewModel); }
            User user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
            if (user == null)
            {
                TempData["Message"] = Jobs.CreateMessage(
                    "Hata!",
                    "Kullanıcı bilgisi bulunamadı, lütfen tekrar deneyiniz!",
                    "warning");
                return Redirect("/");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);
            if(result.Succeeded)
            {
                TempData["Message"] = Jobs.CreateMessage(
                    "Başarılı!",
                    "Parolanız, başarıyla değiştirilmiştir! Giriş yapmayı deneyebilirsiniz.",
                    "success");
                return RedirectToAction("Login");
            }
            TempData["Message"] = Jobs.CreateMessage(
                "Bir sorun oluştu!",
                "Beklenmedik bir sorun oluştu, kesin Canan'dan kaynaklıdır!",
                "danger");
            return View(resetPasswordViewModel);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
