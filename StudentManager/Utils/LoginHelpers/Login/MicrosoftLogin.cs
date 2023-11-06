using Microsoft.AspNetCore.Identity;
using StudentManager.Utils.LoginHelpers;
using StudentManager.Utils.LoginHelpers.Providers;

namespace StudentManager.Controllers;

public class MicrosoftLogin : ExternalLogin<IdentityUser>
{
    public ProviderUser user { get; }
    public string Provider { get; }
    public Task<IdentityUser?> MatchExistingUser(UserManager<IdentityUser> userManager)
    {
        throw new NotImplementedException();
    }

    public static async Task<ExternalLogin<IdentityUser>> fromCode(string? code)
    {
        var accessToken = await MicrosoftProvider.exchange_code(code);
        var user = await MicrosoftProvider.getUserInfo(accessToken.AccessToken);
        
        throw new NotImplementedException();
    }
}