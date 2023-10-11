using StudentManager.Models.DBUtils;

namespace StudentManager.Models;

public class EFStudentRepository : IStudentRepository
{
    public EFStudentRepository(StudentManagementDb db)
    {
        Db = db;
    }
    public StudentManagementDb Db { get; set; }

    public IQueryable<Student> Students => Db.Students;
    public void Insert(Student model)
    {
        Db.Students.Add(model);
    }
}