using StudentManager.Utils.LoginHelpers;

namespace StudentManager.Models.Json;

using System;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class MicrosoftUser : ProviderUser
{
    [JsonPropertyName("businessPhones")]
    public string[] BusinessPhones { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("givenName")]
    public string GivenName { get; set; }

    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; }

    [JsonPropertyName("mail")]
    public string Email { get; set; }

    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; }

    [JsonPropertyName("officeLocation")]
    public string OfficeLocation { get; set; }

    [JsonPropertyName("preferredLanguage")]
    public string PreferredLanguage { get; set; }

    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [JsonPropertyName("userPrincipalName")]
    public string UserPrincipalName { get; set; }
    
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    //ProviderUSer
    public string Name => DisplayName;
    
    IFormattable ProviderUser.Id => Id;
}