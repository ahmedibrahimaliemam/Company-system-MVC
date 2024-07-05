using Company.DL.Models;
using Company.PL.Helper;
using Company.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Threading.Tasks;

namespace Company.PL.Controllers.AccountController
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationModel> userManager;
		private readonly SignInManager<ApplicationModel> signInManager;

		public AccountController(UserManager<ApplicationModel> userManager,SignInManager<ApplicationModel> signInManager )
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationModel User = new ApplicationModel()
                {
                    FName = model.Email,
                    LName=model.LName,
                    Agree=model.Agree,
                    Email=model.Email,
                    UserName = model.Email.Split("@")[0],
                    
                };
                var result= await userManager.CreateAsync(User,model.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(model);

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User=await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    bool flag = await userManager.CheckPasswordAsync(User, model.Password);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    ModelState.AddModelError(string.Empty, "InValid Password");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Is InValid");
                }
            }
            return View(model);
        }
        public new async  Task<IActionResult>  SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgotPasswordViewModel model)
        {
            var user= await userManager.FindByEmailAsync(model.Email);
            if(user is not null)
            {
                var Token=await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink=Url.Action("ResetPassword","Account",new { Email=user.Email ,token=Token },Request.Scheme);
                var Email = new EmailModel()
                {
                    To = user.Email,
                    Subject = "ResetPassword",
                    Body = passwordResetLink,
                };
                EmailHelper.SendEmail(Email);
                return RedirectToAction(nameof(CheckYourInbox));

            }
            return View(model);

        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["Email"]=email;
            TempData["Token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            string email = TempData["Email"] as string;
            string Token = TempData["Token"] as string;
            var user= await userManager.FindByEmailAsync(email);
            if(user is not null)
            {
                var result = await userManager.ResetPasswordAsync(user, Token, model.NewPassword);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }
        
       


	}
}
