using System.Collections;

namespace StudentManager.Utils;

public class SimpleRepository<T> : IRepository<T,int>  where T : PartialUpdate<T>, IndexedModel
{
    Dictionary<int, T> _models = new();
    
    public IEnumerable<T> SelectAll()
    {
        return _models.Select((value) => value.Value);
    }
    
    public T Select(int model)
    {
        if (!_models.ContainsKey(model))
        {
            throw new KeyNotFoundException();
        }
        
        return _models.First((valuePair)=>valuePair.Key.Equals(model)).Value;
    }
    //insert
    public bool Insert(T model)
    {
        model.generateId();
        return _models.TryAdd(model.Id, model);
    }
    //Delete
    public void Delete(int id)
    {
        _models.Remove(id);
    }
    
    public void Update(T model)
    {
        if (!_models.ContainsKey(model.Id))
        {
            throw new KeyNotFoundException();
        }
        
        _models[model.Id].Update(model);   
    }
    
    public bool Contains(int model)
    {
        return _models.ContainsKey(model);
    }
}