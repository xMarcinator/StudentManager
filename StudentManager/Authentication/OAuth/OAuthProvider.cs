using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using StudentManager.Utils.LoginHelpers;
using StudentManager.Utils.LoginHelpers.DefaultClass;
using StudentManager.Utils.LoginHelpers.Providers;

namespace StudentManager.Authentication;

public abstract class OAuthProvider
{
        public const string Provider = "Microsoft";
    public const string scope = "read:user user:email";

    protected abstract string ClientId { get; }
    
    protected abstract string ClientSecret { get; }
    
    
    private static HttpClient client = null;
    
    protected static HttpClient clientI()
    {
        if (client == null)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "StudentManager");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        
        return client;
    }
    
    protected abstract string redirectURL { get; }
    protected abstract Dictionary<string, string> redirectParams { get; }

    private static string? URL;
    public string GetUrl(string callbackUrl)
    {
        if (URL == null)
        {
            URL = QueryHelpers.AddQueryString(redirectURL, redirectParams);
        }
        
        return QueryHelpers.AddQueryString(URL, "redirect_uri", callbackUrl);
    }
    
    protected abstract string exchangeUrl { get; }
    protected abstract Dictionary<string, string> exchangeParams { get; }

    public async Task<IOAuthToken> exchange_code(string code)
    {
        var client = clientI();

        var param = exchangeParams;
        
        param["code"] = code;

        var newUrl = new Uri(QueryHelpers.AddQueryString(exchangeUrl, exchangeParams));
        
        var request = new HttpRequestMessage(HttpMethod.Post, newUrl.AbsoluteUri);
        
        var response = await client.SendAsync(request);

        var result = await ProcessExchange(response) 
                     ?? throw new InvalidOperationException();
        
        //Console.WriteLine(result);
        return result;
    }

    public virtual async Task<IOAuthToken?> ProcessExchange(HttpResponseMessage oauthToken)
    {
        return await oauthToken.Content.ReadFromJsonAsync<DefaultAccessToken>();
    }
    
    protected abstract string UserUrl { get; }
    protected abstract Dictionary<string, string> UserParams { get; }
    
    public async Task<ProviderUser?> getUserInfo(IOAuthToken oauthToken)
    {
        var client = clientI();

        var newUrl = new Uri(QueryHelpers.AddQueryString(UserUrl, UserParams));
        var request = new HttpRequestMessage(HttpMethod.Get, newUrl.AbsoluteUri);
        
        request.Headers.Add("Authorization", "Bearer " + oauthToken.AccessToken);
        
        var response = await client.SendAsync(request);
        
        var result = await ProcessUserInfo(response) 
                     ?? throw new InvalidOperationException();
      
        //Console.WriteLine(result);
        return result;
    }
    public abstract Task<ProviderUser?> ProcessUserInfo(HttpResponseMessage oauthToken);
    
    public virtual async Task<IdentityUser?> MatchExistingUser(IEnumerable<IdentityUser> users)
    {
        return null;
    }
}