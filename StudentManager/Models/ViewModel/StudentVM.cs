namespace StudentManager.Models;

public class StudentVM
{
    public required List<Student> Students { get; set; }
    public String SearchString { get; set; } = "";
}