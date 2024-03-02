using Microsoft.AspNetCore.Identity;

namespace StudentManager.Utils.LoginHelpers;

public interface ExternalLogin<T> where T : class
{
    public ProviderUser user { get; }
    public string Provider { get; }
    public Task<IdentityUser?> MatchExistingUser(UserManager<T> userManager);
}