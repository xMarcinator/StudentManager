namespace StudentManager.Utils;

public static class Extensions{
    public static bool IsNotNull<T>(this T? obj, out T value)
    {
        if (obj is null)
        {
            value = default!;
            return false;
        }
    
        value = obj;
        return true;
    }
}