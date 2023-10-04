namespace StudentManager.Utils;

public interface IndexedModel
{
    public int Id { get; }
    public void generateId();
}