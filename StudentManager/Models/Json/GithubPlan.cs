using System.Text.Json.Serialization;

namespace StudentManager.Models.Json;

public partial class GithubPlan
{
    [JsonPropertyName("collaborators")] public long Collaborators { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("private_repos")] public long PrivateRepos { get; set; }
    [JsonPropertyName("space")] public long Space { get; set; }
}