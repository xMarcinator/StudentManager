using Microsoft.EntityFrameworkCore;
using StudentManager.Models.DBUtils;

namespace StudentManager.Models;

public class EFClassRepository : EFBaseRepository<ClassModel>
{
    public EFClassRepository(StudentManagementDb db) : base(db)
    {
    }

    private protected override DbSet<ClassModel> TargetDbSet => Db.Classes;
}