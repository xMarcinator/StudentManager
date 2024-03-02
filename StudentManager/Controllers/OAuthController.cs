using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Authentication;
using StudentManager.Models.ControllerModels;
using StudentManager.Utils.LoginHelpers;
using StudentManager.Utils.LoginHelpers.Providers;

namespace StudentManager.Controllers;

public class OAuthController : Controller
{
    private UserManager<IdentityUser> userManager;
    private SignInManager<IdentityUser> signInManager;
    private OAuthProviderService providerService;
    
    public OAuthController(UserManager<IdentityUser> userMgr,
        SignInManager<IdentityUser> signInMgr,OAuthProviderService providerService){
        userManager = userMgr;
        signInManager = signInMgr;
        this.providerService = providerService;
    }
    
    public IActionResult Github(string? returnUrl) {
        if (returnUrl == null)
            returnUrl = "/";
        
        string actionUrl = Url.ActionLink("GithubCallback", "OAuth", new { returnUrl },Request.Scheme)!;
        
        var url = GithubProvider.GetUrl(actionUrl);
        return Redirect(url);
    }
    
    public async Task<IActionResult> GithubCallback(string? code,string? returnUrl,GithubErrorType? error, string? error_description,string? error_uri) {
        if (error != null)
        {
            switch (error)
            {
                case GithubErrorType.application_suspended:
                    Console.WriteLine("application_suspended");
                    break;
                case GithubErrorType.redirect_uri_mismatch:
                    Console.WriteLine("redirect_uri_mismatch");
                    break;
                case GithubErrorType.access_denied:
                    Console.WriteLine("access_denied");
                    break;
            }
            
            return Redirect(Url.Action("Login", "Account",new { error = error_description }));
        }
        
        return await ExternalLogin(await GithubLogin.fromCode(code),returnUrl);
    }
    
    public async Task<IActionResult> MicrosftCallback(string? code,string? returnUrl,GithubErrorType? error, string? error_description,string? error_uri) {
        if (error != null)
        {
            switch (error)
            {
                case GithubErrorType.application_suspended:
                    Console.WriteLine("application_suspended");
                    break;
                case GithubErrorType.redirect_uri_mismatch:
                    Console.WriteLine("redirect_uri_mismatch");
                    break;
                case GithubErrorType.access_denied:
                    Console.WriteLine("access_denied");
                    break;
            }
            
            return Redirect(Url.Action("Login", "Account",new { error = error_description }));
        }
        
        return await ExternalLogin(await MicrosoftLogin.fromCode(code),returnUrl);
    }
    
    private async Task<IActionResult> ExternalLogin(ExternalLogin<IdentityUser> provider,string returnUrl)
    {
        var userIndentity = await userManager.FindByLoginAsync(provider.Provider, provider.user.Id.ToString());
        
        //User not found then you need to link account
        if (userIndentity == null)
        {
            //Try to find user using login
            userIndentity = await provider.MatchExistingUser(userManager);
            
            //If user not found then create new user
            if (userIndentity == null)
            {
                userIndentity = new IdentityUser(provider.user.Name);
                userIndentity.Email = provider.user.Email;
                await userManager.CreateAsync(userIndentity);
            }
            //add login to existing user
            await userManager.AddLoginAsync(userIndentity, new UserLoginInfo(provider.Provider, provider.user.Id.ToString(), provider.Provider));
        }

        if (await signInManager.CanSignInAsync(userIndentity))
        {
            await signInManager.SignInAsync(userIndentity, true);
            return Redirect(returnUrl ?? "/");
        }
        else
        {
            return Redirect("/");
        }
    }
}