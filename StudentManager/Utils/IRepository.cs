namespace StudentManager.Utils;

public interface IRepository<T,TK>
{
    public IEnumerable<T> SelectAll();

    public T Select(TK id);
    //insert
    public bool Insert(T model);
    //Delete
    public void Delete(TK id);

    public void Update(T model);

    public bool Contains(TK id);
}