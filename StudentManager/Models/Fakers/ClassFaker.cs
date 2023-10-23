using Bogus;

namespace StudentManager.Models.Fakers;

public sealed class ClassFaker : Faker<ClassModel>
{
    public ClassFaker(bool includeID = false)
    {
        RuleFor(o => o.Name, f => f.Name.JobArea());
        RuleFor(o => o.Education, _ => null!);
        RuleFor(o => o.ClassCourses, _ =>new List<Course>());
        RuleFor(o => o.Students, _ => new List<Student>());
        
        if (includeID)
        { 
            RuleFor(o => o.Id, f => f.IndexFaker);
        }
    }
}