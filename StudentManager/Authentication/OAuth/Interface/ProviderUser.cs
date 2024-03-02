namespace StudentManager.Utils.LoginHelpers;

public interface ProviderUser
{
    public string Name { get; }
    public string? Email { get; }
    public IFormattable Id { get; }
}