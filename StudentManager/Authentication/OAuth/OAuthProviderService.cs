using StudentManager.Utils.LoginHelpers;
using StudentManager.Utils.LoginHelpers.Providers;

namespace StudentManager.Authentication;

public class OAuthProviderService
{
    public OAuthProviderService()
    {
        var type = typeof(OAuthProvider);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p));


        foreach (var VARIABLE in types)
        {
            Console.WriteLine(VARIABLE.Name);
        }
    }
    
    
}