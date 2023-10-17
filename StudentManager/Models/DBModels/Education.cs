namespace StudentManager.Models;

public class Education
{
    public string Name { get; set; } = "";
    public int Id { get; set; }
    public List<Course> Courses { get; set; } = new();
    
    public List<ClassModel> Classes { get; set; } = new();
    public int SemesterCount { get; set; }
}