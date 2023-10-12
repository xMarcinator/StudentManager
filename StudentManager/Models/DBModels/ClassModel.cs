using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentManager.Models;

[Table("Class")]
[PrimaryKey("Id")]
public class ClassModel
{
    public string Name {get; set;}
    public int Id {get; set;}
    public int Semester {get; set;}
    [Column(TypeName="Date")]
    public DateOnly StartDate {get; set;}
    public List<Student> Students {get; set;} = new List<Student>();
    public List<Course> Courses { get; set; } = new List<Course>();
    public Education Education { get; set; }
}