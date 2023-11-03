namespace StudentManager.Utils.LoginHelpers;

public interface ILoginProvider
{
    public string Provider { get; }
    public string ProviderKey { get; }
}