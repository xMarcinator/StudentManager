using Bogus;

namespace StudentManager.Models.Fakers;

public class ClassFaker
{
    private static readonly string?[] Icons = new[] { "***REMOVED***", "***REMOVED***", null };
    private static Faker<ClassModel>? _studentFaker;
    private static Faker<ClassModel>? _studentTestFaker;

    private static Faker<ClassModel> I(bool testing = false)
    {
        _studentFaker ??= new Faker<ClassModel>()
            .RuleFor(o => o.Name, f => f.Name.JobArea())
            .RuleFor(o => o.Education, f => null)
            .RuleFor(o => o.Courses, f => new List<Course>())
            .RuleFor(o => o.Students, f => new List<Student>());
        
        return _studentFaker;
    }
    
    private static Faker<ClassModel> iTest()
    {
        _studentTestFaker ??= I().Clone().RuleFor(o => o.Id, f => f.IndexGlobal);
        
        return _studentTestFaker;
    }

    public static List<ClassModel> Fake(int count, Boolean fakeForeign = false)
    {
        var tmp = ClassFaker.I().Generate(count);
        return tmp;
    }
    
    public static List<ClassModel> FakeTest(int count, Boolean fakeForeign = false)
    {
        var tmp = ClassFaker.iTest().Generate(count);
        return tmp;
    }
}