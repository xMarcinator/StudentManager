namespace StudentManager.Models;

public class StudentEditVM
{
    public Student student { get; set; }
    public IEnumerable<Education> possibleEducations { get; set; }
}