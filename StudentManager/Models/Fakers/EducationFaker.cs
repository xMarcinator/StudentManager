using Bogus;

namespace StudentManager.Models.Fakers;

public class EducationFaker
{
    private static readonly string?[] Icons = new[] { "***REMOVED***", "***REMOVED***", null };
    private static Faker<Education>? _studentFaker;
    private static Faker<Education>? _studentTestFaker;

    private static Faker<Education> I(bool testing = false)
    {
        _studentFaker ??= new Faker<Education>()
            .RuleFor(o => o.Name, f => f.Name.JobArea())
            .RuleFor(o => o.SemesterCount, f => f.Random.Int(1, 5));
        
        return _studentFaker;
    }
    
    private static Faker<Education> iTest()
    {
        _studentTestFaker ??= I().Clone().RuleFor(o => o.Id, f => f.IndexGlobal);
        
        return _studentTestFaker;
    }

    public static List<Education> Fake(int count, Boolean fakeForeign = false)
    {
        var tmp = EducationFaker.I().Generate(count);
        return tmp;
    }
    
    public static List<Education> FakeTest(int count, Boolean fakeForeign = false)
    {
        var tmp = EducationFaker.iTest().Generate(count);
        return tmp;
    }
}