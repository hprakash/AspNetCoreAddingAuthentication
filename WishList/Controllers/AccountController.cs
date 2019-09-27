using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            var user = new ApplicationUser();
            user.Email = model.Email;
            user.UserName = model.Email;
            string password = model.Password;

            var result = _userManager.CreateAsync(user, password);
            if (result.Result.Succeeded == false)
            {
                foreach (var err in result.Result.Errors)
                {
                    ModelState.AddModelError("Password", err.Description);
                }

                return View("Register", model);
            }


            return RedirectToAction("HomeController.Index");
        }
    }
}
