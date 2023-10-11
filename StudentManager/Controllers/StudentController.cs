using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Utils;

namespace StudentManager.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepository _repo;
    
    public StudentController(IStudentRepository repo)
    {
        _repo = repo;
    }
    
    public IEnumerable<Student> GetStudentList(string? searchString)
    {
        var students = _repo.Students;
        
        if (!string.IsNullOrEmpty(searchString))
        {
            students = students.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
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
        
        if (_repo.Students.SingleOrDefault(b => b.Id == model.Id).IsNotNull(out var dbModel))
            dbModel.Update(model);
        else
            _repo.Insert(model);
        
        return RedirectToAction("List");
    }
    
    
    public RedirectToActionResult Delete(int id)
    {
        _repo.Delete(id);
        
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        var model = id.HasValue ? _repo.Select(id.Value) :  new Student();
        return View(model);
    }
    
    [HttpPost]
    public IActionResult Edit(Student model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        if (model.Id != 0)
            _repo.Update(model);
        else
            _repo.Insert(model);
        
        return RedirectToAction("List");
    }
}