namespace StudentManager.Utils;

public class PartialUpdate<T>
{
    public void Update(T updatedModel)
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(updatedModel);
            if (value != null)
            {
                property.SetValue(this, value);
            }
        }
    }
}