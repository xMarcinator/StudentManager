using System.Text.Json.Serialization;

namespace StudentManager.Models.Json;

/// <summary>
/// Email
/// </summary>
public class EmailInfo
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("primary")]
    public bool Primary { get; set; }

    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    [JsonPropertyName("visibility")]
    public string Visibility { get; set; }
}