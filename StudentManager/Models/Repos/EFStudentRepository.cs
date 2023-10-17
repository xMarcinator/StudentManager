using Microsoft.EntityFrameworkCore;
using StudentManager.Models.DBUtils;

namespace StudentManager.Models;

public class EFStudentRepository : EFBaseRepository<Student>
{
    public EFStudentRepository(StudentManagementDb db) : base(db)
    {
    }

    private protected override DbSet<Student> TargetDbSet => Db.Students;
}