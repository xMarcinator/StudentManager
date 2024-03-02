namespace StudentManager.Utils.LoginHelpers.Providers;

public interface IOAuthToken
{
    public string AccessToken { get; set; }

    public string Scope { get; set; }
}