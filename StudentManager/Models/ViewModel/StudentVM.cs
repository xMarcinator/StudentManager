namespace StudentManager.Models;

public class StudentVM
{
    public required IEnumerable<Student> Students { get; set; }
    public String? SearchString { get; set; } = "";
}