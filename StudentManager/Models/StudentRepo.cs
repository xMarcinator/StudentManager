using StudentManager.Utils;

namespace StudentManager.Models;

public class StudentRepo : SimpleRepository<Student>
{
    public StudentRepo()
    {
        Student[] models = new[]
        {
            new Student()
            {
                ProfilePicture = "***REMOVED***",
                Name = "***REMOVED*** Doe",
                Education = "Computer Science",
                Semester = 3,
                Email = "johndoe@gmail.com"
            },
            new Student()
            {
                ProfilePicture = "***REMOVED***",
                Name = "***REMOVED*** Doe",
                Education = "Computer Science",
                Semester = 3,
                Email = "johndoe@gmail.com"
            },
            new Student()
            {
                Name = "John Doe",
                Education = "Computer Science",
                Semester = 3,
                Email = "johndoe@gmail.com"
            }
        };
        
        foreach (var model in models)
        {
            this.Insert(model);
        }
    }
}