using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManager.Models;

public class Student
{
    [Key] public int Id { get; set; }

    public string? ProfilePicture { get; set; }

    [Required(ErrorMessage = "Please specify a first name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please specify a last name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please specify a Email")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Column(TypeName = "nvarchar(255)")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please specify a Class")]
    public int ClassId { get; set; }
    public ClassModel? Class { get; set; }

    public List<Course> ExplicitCourses { get; set; } = new();
}