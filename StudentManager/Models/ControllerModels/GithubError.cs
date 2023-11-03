namespace StudentManager.Models.ControllerModels;

public enum GithubErrorType
{
    application_suspended,
    invalid_client,
    invalid_grant,
    unauthorized_client,
    unsupported_grant_type,
    invalid_scope,
    redirect_uri_mismatch,
    access_denied,
}

public class GithubError
{
    public GithubErrorType? error { get; set; }
    public string? error_description { get; set; }
    
    public string? error_uri { get; set; }
}