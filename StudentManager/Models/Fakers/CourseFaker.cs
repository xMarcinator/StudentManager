using Bogus;

namespace StudentManager.Models.Fakers;

public sealed class CourseFaker : Faker<Course>
{
    public CourseFaker(bool IncludeID = false)
    {
        RuleFor(o => o.Name, f => f.Name.JobTitle());
        RuleFor(b => b.Educations, _ => new List<Education>());
        
        if (IncludeID)
        { 
            RuleFor(o => o.Id, f => f.IndexFaker);
        }
    }
}