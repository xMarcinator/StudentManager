using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManager.Models;

[Table("Class")]
[PrimaryKey("Id")]
public class ClassModel
{
    [Key]
    public int Id {get; set;}
    public string Name {get; set;}
    public int Semester {get; set;}
    [Column(TypeName="Date")]
    public DateOnly StartDate {get; set;}
    public List<Student> Students {get; set;} = new List<Student>();
    public List<Course> ClassCourses { get; set; } = new List<Course>();
    
    public int EducationId { get; set; }
    public Education Education { get; set; }
}