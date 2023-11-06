using Microsoft.AspNetCore.WebUtilities;
using StudentManager.Models.Json;

namespace StudentManager.Utils.LoginHelpers.Providers;

public static class MicrosoftProvider
{
    public const string Provider = "Microsoft";
    public const string scope = "read:user user:email";

    
    private static string ClientId =>
        Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_ID") ?? throw new InvalidOperationException();

    private static string ClientSecret =>
        Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_SECRET") ?? throw new InvalidOperationException();
    
    private static string Tenant =>
        Environment.GetEnvironmentVariable("MICROSOFT_TENANT_ID") ?? throw new InvalidOperationException();

    private static HttpClient client = null;
    
    private static HttpClient clientI()
    {
        if (client == null)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "StudentManager");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        
        return client;
    }
    
    public static async Task<MicrosoftAccessToken> exchange_code(string code)
    {
        var client = clientI(); 
       
        string url = $"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/token";
        Dictionary<string, string> param = new Dictionary<string, string>
        {
            { "client_id", ClientId }, 
            { "client_secret", ClientSecret }, 
            { "code", code } , 
            { "scope", scope }
        };
        var newUrl = new Uri(QueryHelpers.AddQueryString(url, param));
        
        var request = new HttpRequestMessage(HttpMethod.Post, newUrl.AbsoluteUri);
        
        var response = await client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<MicrosoftAccessToken>() 
                     ?? throw new InvalidOperationException();
        
        //Console.WriteLine(result);
        return result;
    }
    
    public static async Task<MicrosoftUser> getUserInfo(string oauthToken)
    {
        var client = clientI();
        const string url = "https://graph.microsoft.com/v1.0/me";
        
        var newUrl = new Uri(url);
        var request = new HttpRequestMessage(HttpMethod.Get, newUrl.AbsoluteUri);
            
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "Bearer " + oauthToken);
        
        var response = await client.SendAsync(request);
        
        var result = await response.Content.ReadFromJsonAsync<MicrosoftUser>();
        
        if (result == null) throw new InvalidOperationException();
        
        //Console.WriteLine(result);
        return result;
    }

    public static async Task<EmailInfo[]> getUserEmails(string oauthToken)
    {
        var client = clientI();
        const string url = "https://api.github.com/user/emails";
        
        var newUrl = new Uri(url);
        var request = new HttpRequestMessage(HttpMethod.Get, newUrl.AbsoluteUri);
            
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "Bearer " + oauthToken);
        
        var response = await client.SendAsync(request);
        
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ReasonPhrase);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            throw new InvalidOperationException();
        }
        
        var result = await response.Content.ReadFromJsonAsync<EmailInfo[]>() ?? throw new InvalidOperationException();
        
        //Console.WriteLine(result);
        return result;
    }

    public static async Task<EmailInfo> getUserPrimaryEmails(string oauthToken)
    {
        var emails = await getUserEmails(oauthToken);
        
        return emails.First(email => email.Primary);
    }

    private static string? URL;
    public static string GetUrl(string callbackUrl)
    {
        if (URL == null)
        {
            const string baseURL = "https://github.com/login/oauth/authorize";
            const string baseScopes = "read:user user:email";

            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                { "response_type", "authorization_code" },
                { "client_id", ClientId },
                { "scope", baseScopes },
            };
            
            URL = QueryHelpers.AddQueryString(baseURL, param);
        }
        
        return QueryHelpers.AddQueryString(URL, "redirect_uri", callbackUrl);
    }
}