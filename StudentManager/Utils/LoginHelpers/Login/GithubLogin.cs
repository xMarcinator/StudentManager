using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models.Json;
using StudentManager.Utils.LoginHelpers.Providers;

namespace StudentManager.Utils.LoginHelpers;

public class GithubLogin : ExternalLogin<IdentityUser>
{
    public ProviderUser user => githubUser;
    public string Provider { get; set; }
    
    public async Task<IdentityUser?> MatchExistingUser(UserManager<IdentityUser> userManager)
    {
        var user = await userManager.FindByNameAsync(githubUser.Name);
        
        if (user == null && githubUser.Email != null)
            user = await userManager.FindByEmailAsync(githubUser.Email);

        if (user == null)
        {
            var emails = await GithubProvider.getUserEmails(GithubAccessToken.access_token);
            
            IdentityUser foundUser = null;
            
            foreach (var currentUser in userManager.Users)
            {
                var email = emails.FirstOrDefault((info => info.Email == currentUser.Email));
                if (email == null)
                    continue;

                // If the email is verified, then we can use it
                if (email.Verified)
                {
                    foundUser = currentUser;
                    // If the email is primary, then we can stop looking
                    if (email.Primary)
                        break;
                }
            }
            
            user = foundUser;
        }
        return user;
    }
    
    public GithubUser githubUser { get; set; }
    public GithubAccessToken GithubAccessToken { get; set; }

    public static async Task<GithubLogin> fromCode(string code)
    {
        var accessToken = await GithubProvider.exchange_code(code);
        var userInfo = await GithubProvider.getUserInfo(accessToken.access_token);

        return new GithubLogin()
        {
            githubUser = userInfo,
            GithubAccessToken = accessToken,
            Provider = GithubProvider.Provider,
        };
    }
}