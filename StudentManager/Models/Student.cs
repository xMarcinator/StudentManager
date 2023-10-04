using System.ComponentModel.DataAnnotations;
using StudentManager.Utils;

namespace StudentManager.Models;

public class Student : PartialUpdate<Student>, IndexedModel
{
    public static int NextId;
    public int Id { get; set; }
    
    public void generateId()
    {
        Id = ++NextId;
    }

    public string? ProfilePicture { get; set; }
    
    [Required(ErrorMessage = "Please specify a name")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Education is required")]
    public string? Education { get; set; }
    
    [Required(ErrorMessage = "Semester is required")]
    [Range(1,10)]
    public int Semester { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [RegularExpression(pattern:"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
    public string? Email { get; set; }
}