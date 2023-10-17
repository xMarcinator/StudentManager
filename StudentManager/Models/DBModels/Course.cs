namespace StudentManager.Models;

public class Course
{
    public string Name { get; set; } = "";
    public int Id { get; set; }
    public List<Student> ExplicitStudents { get; set; } = new();
    public List<Education> Educations { get; set; } = new();
    public List<ClassModel> Classes { get; set; } = new();
}