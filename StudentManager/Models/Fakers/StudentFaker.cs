using Bogus;

namespace StudentManager.Models.Fakers;

public static class FakeStudent
{
    private static readonly string?[] Icons = new []{"***REMOVED***", "***REMOVED***", null};
    private static Faker<Student>? _studentFaker;

    public static Faker<Student> i()
    {
            _studentFaker ??= new Faker<Student>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(true)
                //OrderId is deterministic
                .RuleFor(o => o.Email, f => f.Internet.Email())
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.ProfilePicture, f => f.PickRandom(Icons));

        return _studentFaker;
    }
    
    public static List<Student> FakeStudents(int count, Boolean fakeForeign = false)
    {
        var tmp = FakeStudent.i().Generate(count);
        

        return tmp;
    }
}