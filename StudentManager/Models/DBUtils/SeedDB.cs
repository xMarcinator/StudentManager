using Microsoft.EntityFrameworkCore;
using StudentManager.Models.Fakers;

namespace StudentManager.Models.DBUtils;

public class SeedDb
{
    private const int classSize = 2;
    private const int classCount = 3;
    
    public static void EnsurePopulated(IApplicationBuilder app)
    {
        StudentManagementDb context = app.ApplicationServices.CreateScope().ServiceProvider
            .GetRequiredService<StudentManagementDb>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        if (!context.Students.Any())
        {
            var fakeStudents = new StudentFaker().Generate(classSize*classCount);
            var fakeClasses = new ClassFaker().Generate(classCount);
            var fakeEducations = new EducationFaker().Generate(1);
            var fakeCourses = new CourseFaker().Generate(2);

            var rng = new Random();
            
            for (int i = 0; i < fakeClasses.Count; i++)
            {
                fakeClasses[i].Education = fakeEducations[0];

                fakeClasses[i].Education.Courses = fakeCourses;

                fakeClasses[i].Semester = rng.Next(fakeClasses[i].Education.SemesterCount);
                
                fakeClasses[i].Students.AddRange(fakeStudents.GetRange(i * classSize,classSize));
            }
            
            context.Students.AddRange(fakeStudents);
            context.Classes.AddRange(fakeClasses);
            context.Educations.AddRange(fakeEducations);
            
            context.SaveChanges();
        }
    }
}