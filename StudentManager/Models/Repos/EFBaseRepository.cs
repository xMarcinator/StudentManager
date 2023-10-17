using Microsoft.EntityFrameworkCore;
using StudentManager.Models.DBUtils;

namespace StudentManager.Models;

public abstract class EFBaseRepository<T>: IModelRepository<T> where T : class
{
    protected EFBaseRepository(StudentManagementDb db)
    {
        Db = db;
    }

    protected StudentManagementDb Db { get; set; }

    private protected abstract DbSet<T> TargetDbSet {get;}

    public bool AutoSave { get; set; } = true;
    public IQueryable<T> Models => TargetDbSet;
    public void Insert(T model)
    {
        TargetDbSet.Add(model);
        
        if (AutoSave) SaveChanges();
    }

    public void Update(T model)
    {
        TargetDbSet.Update(model);
        
        if (AutoSave) SaveChanges();
    }

    public void Delete(T model)
    {
        TargetDbSet.Remove(model);
        
        if (AutoSave) SaveChanges();
    }

    public void SaveChanges()
    {
        Db.SaveChanges();
    }
}