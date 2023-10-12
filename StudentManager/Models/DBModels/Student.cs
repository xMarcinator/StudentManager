using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManager.Utils;

namespace StudentManager.Models;

public class Student
{
    [Key]
    public int Id { get; set; }
    public string? ProfilePicture { get; set; }

    [Required(ErrorMessage = "Please specify a first name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please specify a last name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Education is required")]
    public string Education { get; set; }

    [Required(ErrorMessage = "Semester is required")]
    [Range(1, 10)]
    [Column(TypeName = "tinyint")]
    public int Semester { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Column(TypeName = "nvarchar(255)")]
    public string Email { get; set; }
    
    public ClassModel Classes { get; set; } = new();
}