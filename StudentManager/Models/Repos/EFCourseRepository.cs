using Microsoft.EntityFrameworkCore;
using StudentManager.Models.DBUtils;

namespace StudentManager.Models;

public class EFCourseRepository : EFBaseRepository<Course>
{
    public EFCourseRepository(StudentManagementDb db) : base(db)
    {
    }

    private protected override DbSet<Course> TargetDbSet => Db.Courses;
}