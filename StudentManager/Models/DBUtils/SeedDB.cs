using Microsoft.EntityFrameworkCore;

namespace StudentManager.Models.DBUtils;

public class SeedDb
{
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
            context.Students.AddRange(new Student()
                {
                    ProfilePicture = "***REMOVED***",
                    Name = "***REMOVED*** Doe",
                    Education = "Computer Science",
                    Semester = 3,
                    Email = "johndoe@gmail.com"
                }, new Student()
                {
                    ProfilePicture = "***REMOVED***",
                    Name = "***REMOVED*** Doe",
                    Education = "Computer Science",
                    Semester = 3,
                    Email = "johndoe@gmail.com"
                }, new Student()
                {
                    Name = "John Doe",
                    Education = "Computer Science",
                    Semester = 3,
                    Email = "johndoe@gmail.com"
                }
            );
            
            context.SaveChanges();
        }
    }
}