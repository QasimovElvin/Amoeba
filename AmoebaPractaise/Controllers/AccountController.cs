using AmoebaPractaise.Models;
using AmoebaPractaise.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaPractaise.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            ApplicationUser user = new ApplicationUser()
            {
                UserName = register.UserName,
                Name =register.Name,
                Surname =register.Surname,
                Email=register.Email
            };
            var result = await _userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            if (login.EmailOrUsername.Contains("@"))
            {
                ApplicationUser user=await _userManager.FindByEmailAsync(login.EmailOrUsername);
                if (user != null)
                {
                  var result =await  _signInManager.PasswordSignInAsync(user,login.Password,false,false);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Password is wrong");
                        return View();
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ApplicationUser user = await _userManager.FindByNameAsync(login.EmailOrUsername);
                if (user != null)
                {
                  var result= await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Null");
                        return View();
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
