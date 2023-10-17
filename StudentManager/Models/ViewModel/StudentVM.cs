using StudentManager.Controllers;

namespace StudentManager.Models.ViewModel;

public class StudentVM
{
    public required IEnumerable<Student> Students { get; set; }
    public String? SearchString { get; set; } = "";
    public StudentController.PagingInfo PagingInfo { get; set; }
}