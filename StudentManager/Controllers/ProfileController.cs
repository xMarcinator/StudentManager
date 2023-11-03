using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StudentManager.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private UserManager<IdentityUser> userManager;
    public ProfileController(UserManager<IdentityUser> userMgr) {
        userManager = userMgr;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = await userManager.FindByNameAsync(User.Identity.Name);
        
        return View(user);
    }
}