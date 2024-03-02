using System.Text.Json.Serialization;
using StudentManager.Utils.LoginHelpers.Providers;

namespace StudentManager.Utils.LoginHelpers.DefaultClass;

public class DefaultAccessToken : IOAuthToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}