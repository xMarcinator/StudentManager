using Microsoft.EntityFrameworkCore;
using StudentManager.Models.DBUtils;

namespace StudentManager.Models;

public class EFEducationRepository : EFBaseRepository<Education>
{
    public EFEducationRepository(StudentManagementDb db) : base(db)
    {
    }

    private protected override DbSet<Education> TargetDbSet => Db.Educations;
}