using Bogus;

namespace StudentManager.Models.Fakers;

public class StudentFaker
{
    private static readonly string?[] Icons = new[] { "***REMOVED***", "***REMOVED***", null };
    private static Faker<Student>? _studentFaker;
    private static Faker<Student>? _studentTestFaker;

    public static Faker<Student> i()
    {
        _studentFaker ??= new Faker<Student>()
            .RuleFor(o => o.FirstName, f => f.Name.FirstName())
            .RuleFor(o => o.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName,provider:"eamv.dk"))
            .RuleFor(o => o.ProfilePicture, f => f.PickRandom(Icons))
            .RuleFor(b => b.Class, _ => null);
        
        return _studentFaker;
    }
    
    public static Faker<Student> iTest()
    {
        _studentTestFaker ??= i().Clone().RuleFor(o => o.Id, f => f.IndexGlobal);
        
        return _studentTestFaker;
    }

    public static List<Student> Fake(int count, Boolean fakeForeign = false)
    {
        var tmp = StudentFaker.i().Generate(count);
        return tmp;
    }
    
    public static List<Student> FakeTest(int count, Boolean fakeForeign = false)
    {
        var tmp = StudentFaker.iTest().Generate(count);
        return tmp;
    }
}