using Bogus;

namespace StudentManager.Models.Fakers;

public sealed class EducationFaker : Faker<Education>
{
    public EducationFaker(bool includeId = false)
    {
        RuleFor(o => o.Name, f => f.Name.JobArea());
        RuleFor(o => o.SemesterCount, f => f.Random.Int(1, 5));
        RuleFor(o => o.Courses, _=> new List<Course>());
        
        if (includeId)
        { 
            RuleFor(o => o.Id, f => f.IndexGlobal);
        }
    }
}