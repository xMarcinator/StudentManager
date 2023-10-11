namespace StudentManager.Models.DBUtils;

public interface IModelRepository<T>
{
    public bool AutoSave { get; set; }
    public IQueryable<T> Models { get; }

    public void Insert(T model);
    public void Update(T model);
    public void Delete(T model);

    public void SaveChanges();
    
    
}