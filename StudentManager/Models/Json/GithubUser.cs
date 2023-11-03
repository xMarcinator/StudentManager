using System.Text.Json.Serialization;
using StudentManager.Utils.LoginHelpers;

namespace StudentManager.Models.Json;

/// <summary>
/// Private User
///
/// Public User
/// </summary>
public class GithubUser : ProviderUser
{
    [JsonPropertyName("avatar_url")] public Uri AvatarUrl { get; set; }
    [JsonPropertyName("bio")] public string Bio { get; set; }
    [JsonPropertyName("blog")] public string Blog { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("business_plus")]
    public bool? BusinessPlus { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("collaborators")]
    public long? Collaborators { get; set; }

    [JsonPropertyName("company")] public string Company { get; set; }
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("disk_usage")]
    public long? DiskUsage { get; set; }

    [JsonPropertyName("email")] public string? Email { get; set; }
    [JsonPropertyName("events_url")] public string EventsUrl { get; set; }
    [JsonPropertyName("followers")] public long Followers { get; set; }
    [JsonPropertyName("followers_url")] public Uri FollowersUrl { get; set; }
    [JsonPropertyName("following")] public long Following { get; set; }
    [JsonPropertyName("following_url")] public string FollowingUrl { get; set; }
    [JsonPropertyName("gists_url")] public string GistsUrl { get; set; }
    [JsonPropertyName("gravatar_id")] public string GravatarId { get; set; }
    [JsonPropertyName("hireable")] public bool? Hireable { get; set; }
    [JsonPropertyName("html_url")] public Uri HtmlUrl { get; set; }
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("ldap_dn")]
    public string LdapDn { get; set; }

    [JsonPropertyName("location")] public string Location { get; set; }
    [JsonPropertyName("login")] public string Login { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("node_id")] public string NodeId { get; set; }

    [JsonPropertyName("organizations_url")]
    public Uri OrganizationsUrl { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("owned_private_repos")]
    public long? OwnedPrivateRepos { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("plan")]
    public GithubPlan GithubPlan { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("private_gists")]
    public long? PrivateGists { get; set; }

    [JsonPropertyName("public_gists")] public long PublicGists { get; set; }
    [JsonPropertyName("public_repos")] public long PublicRepos { get; set; }

    [JsonPropertyName("received_events_url")]
    public Uri ReceivedEventsUrl { get; set; }

    [JsonPropertyName("repos_url")] public Uri ReposUrl { get; set; }
    [JsonPropertyName("site_admin")] public bool SiteAdmin { get; set; }
    [JsonPropertyName("starred_url")] public string StarredUrl { get; set; }

    [JsonPropertyName("subscriptions_url")]
    public Uri SubscriptionsUrl { get; set; }

    [JsonPropertyName("suspended_at")] public DateTimeOffset? SuspendedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("total_private_repos")]
    public long? TotalPrivateRepos { get; set; }

    [JsonPropertyName("twitter_username")] public string TwitterUsername { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("two_factor_authentication")]
    public bool? TwoFactorAuthentication { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
    [JsonPropertyName("url")] public Uri Url { get; set; }
}