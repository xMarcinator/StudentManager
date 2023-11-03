using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Utils.LoginHelpers;

namespace StudentManager.Controllers;

public class AccountController : Controller
{
    private UserManager<IdentityUser> userManager;
    private SignInManager<IdentityUser> signInManager;
    public AccountController(UserManager<IdentityUser> userMgr,
        SignInManager<IdentityUser> signInMgr) {
        userManager = userMgr;
        signInManager = signInMgr;
    }
    
    public IActionResult Login(string returnUrl, string? error) {
        if (error != null)
        {
            ModelState.AddModelError("", error);
        }
        
        return View(new LoginModel {
            ReturnUrl = returnUrl
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel) {
        if (ModelState.IsValid) {
            IdentityUser user =
                await userManager.FindByNameAsync(loginModel.Name);
            if (user != null) {
                await signInManager.SignOutAsync();
                var hello = (await signInManager.PasswordSignInAsync(user,
                    loginModel.Password, false, false));
                if (hello.Succeeded) {
                    return Redirect(loginModel?.ReturnUrl ?? "/Admin");
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
        }
        return View(loginModel);
    }

    public IActionResult Logout()
    {
        signInManager.SignOutAsync();
        return Redirect("/");
    }
}

public class LoginModel
{
    public string ReturnUrl { get; set; }
    public string Name { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}