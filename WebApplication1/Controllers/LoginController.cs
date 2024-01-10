using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using WebApplication1.Models;
using WebApplication1.ModelViews;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        
        public LoginController(UserManager<AppUser> userManager = null, IFileProvider fileProvider = null, RoleManager<AppRole> roleManager = null, SignInManager<AppUser> signInManager = null)
        {
            _userManager = userManager;
            _fileProvider = fileProvider;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz Kullanıcı Adı veya Parola!");
                return View();
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Kullanıcı Girişi " + user.LockoutEnd + " kadar kısıtlanmıştır!");
                return View();
            }
            ModelState.AddModelError("", "Geçersiz Kullanıcı Adı veya Parola Başarısız Giriş Sayısı :" + await _userManager.GetAccessFailedCountAsync(user) + "/3");
            return View();
        }
        public IActionResult Register()
        {
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var identityResult = await _userManager.CreateAsync(new() { UserName = model.UserName, Email = model.Email, FullName = model.FullName,}, model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Admin" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index","Login");
        }




    }
}
