using StudentManager.Controllers;

namespace StudentManager.Models.ViewModel;

public class StudentVM
{
    public required IEnumerable<Student> Students { get; set; }
    public String? SearchString { get; set; } = "";
    public int Education { get; set; } = 0;
    public StudentController.PagingInfo PagingInfo { get; set; }
}