using Microsoft.AspNetCore.WebUtilities;
using StudentManager.Authentication;
using StudentManager.Models.Json;

namespace StudentManager.Utils.LoginHelpers.Providers;

public class MicrosoftOAuthProviderCopy : OAuthProvider
{
    public const string Provider = "Microsoft";
    public const string scope = "read:user user:email";
    
    protected override string ClientId =>
        Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_ID") ?? throw new InvalidOperationException();

    protected override string ClientSecret =>
        Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_SECRET") ?? throw new InvalidOperationException();
    
    private static string Tenant =>
        Environment.GetEnvironmentVariable("MICROSOFT_TENANT_ID") ?? throw new InvalidOperationException();
    

    protected override string exchangeUrl => $"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/token";

    protected override Dictionary<string, string> exchangeParams => new()
    {
        { "client_id", ClientId },
        { "client_secret", ClientSecret },
        { "scope", scope }
    };

    public override Task<IOAuthToken> ProcessExchange(HttpResponseMessage oauthToken)
    {
        throw new NotImplementedException();
    }

    protected override string UserUrl => "https://graph.microsoft.com/v1.0/me";
    protected override Dictionary<string, string> UserParams => new();
    public override Task<ProviderUser> ProcessUserInfo(HttpResponseMessage oauthToken)
    {
        throw new NotImplementedException();
    }

    protected override string redirectURL => $"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/authorize";

    protected override Dictionary<string, string> redirectParams => new()
    {
        { "response_type", "authorization_code" },
        { "client_id", ClientId },
        { "scope", scope },
    };
}