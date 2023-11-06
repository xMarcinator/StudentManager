namespace StudentManager.Utils.LoginHelpers;

public interface ProviderUser
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public long Id { get; set; }
}