using Microsoft.AspNetCore.Identity;
using StudentManager.Models.Json;
using StudentManager.Utils.LoginHelpers;
using StudentManager.Utils.LoginHelpers.Providers;

namespace StudentManager.Controllers;

public class MicrosoftLogin : ExternalLogin<IdentityUser>
{
    public ProviderUser user => MicrosoftUser;
    public string Provider { get; }
    
    public MicrosoftAccessToken accessToken { get; }
    public MicrosoftUser MicrosoftUser { get; }
    public Task<IdentityUser?> MatchExistingUser(UserManager<IdentityUser> userManager)
    {
        throw new NotImplementedException();
    }

    public static async Task<ExternalLogin<IdentityUser>> fromCode(string? code)
    {
        var accessToken = await MicrosoftProvider.exchange_code(code);
        var user = await MicrosoftProvider.getUserInfo(accessToken.AccessToken);

        return new MicrosoftLogin()
        {

        };
    }
}