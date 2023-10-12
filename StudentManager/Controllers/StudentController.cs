using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Utils;

namespace StudentManager.Controllers;

public class StudentController : Controller
{
    private readonly IModelRepository<Student> _repoStudent;
    private readonly IModelRepository<Education> _repoEdu;
    
    public StudentController(IModelRepository<Student> repoStudent)
    {
        _repoStudent = repoStudent;
    }
    
    public IEnumerable<Student> GetStudentList(string? searchString)
    {
        var students = _repoStudent.Models
            .Include(o=>o.Class)
            .ThenInclude(o=>o.Education);
        
        if (!string.IsNullOrEmpty(searchString))
        {
            return students.Where(s => s.FirstName.ToLower().Contains(searchString.ToLower())
                                                ||s.LastName.ToLower().Contains(searchString.ToLower()))
                .Take(10);
        }
        
        return students.Take(10);
    }
    
    public IActionResult List(string? searchString)
    {
        var studentVm = new StudentVM
        {
            Students = GetStudentList(searchString),
            SearchString = searchString
        };

        return View(studentVm);
    }
    
    public PartialViewResult GetPartialStudentList(string? searchString)
    {
        var studentVm = new StudentVM
        {
            Students = GetStudentList(searchString),
            SearchString = searchString
        };
        
        return PartialView("StudentListPartial", studentVm);
    }


    [HttpGet]
    public RedirectToActionResult create()
    {
        var newStudent = new Student();

        //_repo.Insert(newStudent);
        
        return RedirectToAction("Edit", new {model = newStudent});
    }
    
    [HttpGet]
    public RedirectToActionResult Save(Student model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Edit", model);
        
        if (_repoStudent.Models.SingleOrDefault(b => b.Id == model.Id).IsNotNull(out var dbModel))
            _repoStudent.Update(model);
        else
            _repoStudent.Insert(model);
        
        return RedirectToAction("List");
    }
    
    
    public RedirectToActionResult Delete(Student model)
    {
        _repoStudent.Delete(model);
        
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        Student? model = null;

        if (!id.HasValue)
        {
            model = _repoStudent.Models
                .Where(model => model.Id == id)
                .Include(o => o.Class)
                .ThenInclude(o => o.Education)
                .Include(o => o.Class)
                .ThenInclude(o => o.Courses)
                .FirstOrDefault();
        }

        model ??= new Student();
           

        var editVM = new StudentEditVM()
        {
            student = model,
            possibleEducations = _repoEdu.Models.Include(o => o.Courses)
        };
        
        return View(editVM);
    }
    
    [HttpPost]
    public IActionResult Edit(Student model)
    {
        if (!ModelState.IsValid)
        {
            var editVM = new StudentEditVM()
            {
                student = model,
                possibleEducations = _repoEdu.Models.Include(o => o.Courses)
            };
            
            return View(editVM);
        }
           
        
        if (model.Id != 0)
            _repoStudent.Update(model);
        else
            _repoStudent.Insert(model);
        
        return RedirectToAction("List");
    }
}