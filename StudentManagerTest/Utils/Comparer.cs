namespace StudentManagerTest.Utils;


public class Comparer
{
    public static Comparer<T> get<T>(Func<T?, T?, bool> equals)
    {
        return new Comparer<T>(equals);
    }
}
public class Comparer<T> : IEqualityComparer<T>
{
    public Func<T?, T?, bool> EqualsLambda { get; set; }
    
    internal Comparer(Func<T, T, bool> equals)
    {
        EqualsLambda = equals;
    }
    public bool Equals(T? x, T? y)
    {
        return EqualsLambda(x, y);
    }

    public int GetHashCode(T obj)
    {
        throw new NotImplementedException();
    }
}