using Bogus;

namespace StudentManager.Models.Fakers;

public sealed class StudentFaker : Faker<Student> {
    private static readonly string?[] Icons = { null };
    
    public StudentFaker(bool includeId = false)
    {
        RuleFor(o => o.FirstName, f => f.Name.FirstName());
        RuleFor(o => o.LastName, f => f.Name.LastName());
        RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName, provider: "eamv.dk"));
        RuleFor(o => o.ProfilePicture, f => f.PickRandom(Icons));
        RuleFor(b => b.Class, _ => null!);
        
        if (includeId)
        { 
            RuleFor(o => o.Id, f => f.IndexGlobal);
        }
    }
}