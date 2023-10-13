using Bogus;

namespace StudentManager.Models.Fakers;

public class CourseFaker
{
   private static Faker<Course>? _studentFaker;
    private static Faker<Course>? _studentTestFaker;

    public static Faker<Course> i()
    {
        _studentFaker ??= new Faker<Course>()
            .RuleFor(o => o.Name, f => f.Name.JobTitle())
            .RuleFor(b => b.Students, _ => null);
        
        return _studentFaker;
    }
    
    public static Faker<Course> iTest()
    {
        _studentTestFaker ??= i().Clone().RuleFor(o => o.Id, f => f.IndexGlobal);
        
        return _studentTestFaker;
    }

    public static List<Course> Fake(int count, Boolean fakeForeign = false)
    {
        var tmp = i().Generate(count);
        return tmp;
    }
    
    public static List<Course> FakeTest(int count, Boolean fakeForeign = false)
    {
        var tmp = iTest().Generate(count);
        return tmp;
    }
}