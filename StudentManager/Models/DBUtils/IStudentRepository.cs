namespace StudentManager.Models.DBUtils;

public interface IStudentRepository
{
    public IQueryable<Student> Students { get; }
    
    public void Insert(Student model);
}