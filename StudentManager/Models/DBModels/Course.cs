namespace StudentManager.Models;

public class Course
{
    public string Name { get; set; } = "";
    public int Id { get; set; }
    public List<ClassModel> Students { get; set; } = new();
    
}