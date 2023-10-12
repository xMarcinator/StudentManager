using StudentManager.Models.DBUtils;

namespace StudentManager.Models;

public class EFStudentRepository : IModelRepository<Student>
{
    public EFStudentRepository(StudentManagementDb db)
    {
        Db = db;
    }
    public StudentManagementDb Db { get; set; }

    public bool AutoSave { get; set; } = true;
    public IQueryable<Student> Models => Db.Students;
    public void Insert(Student model)
    {
        Db.Students.Add(model);
        
        if (AutoSave) SaveChanges();
    }

    public void Update(Student model)
    {
        Db.Students.Update(model);
        
        if (AutoSave) SaveChanges();
    }

    public void Delete(Student model)
    {
        Db.Students.Remove(model);
        
        if (AutoSave) SaveChanges();
    }

    public void SaveChanges()
    {
        Db.SaveChanges();
    }
}