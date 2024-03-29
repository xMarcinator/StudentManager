﻿using System.Text.Json.Serialization;

namespace StudentManager.Utils.LoginHelpers;

public class GithubAccessToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}