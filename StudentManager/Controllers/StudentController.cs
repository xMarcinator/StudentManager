using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Models.ViewModel;
using StudentManager.Utils;

namespace StudentManager.Controllers;

public class StudentController : Controller
{
    private readonly IModelRepository<Student> _repoStudent;
    private readonly IModelRepository<Education> _repoEdu;
    
    public StudentController(IModelRepository<Student> repoStudent,IModelRepository<Education> eduRepo)
    {
        _repoStudent = repoStudent;
        _repoEdu = eduRepo;
    }
    
    public StudentVM GetStudentList(string? searchString,int productPage = 1)
    {
        var paging = new PagingInfo
        {
            CurrentPage = productPage,
            ItemsPerPage = PageSize,
            TotalItems = searchString != null ? 0 : _repoStudent.Models.Count()
        };
        
        var students = (IQueryable<Student>) _repoStudent.Models
            .Include(o=>o.Class)
            .ThenInclude(o=>o.Education);
        
        if (!string.IsNullOrEmpty(searchString))
        {
            students = students.Where(s => s.FirstName.ToLower().Contains(searchString.ToLower())
                                           || s.LastName.ToLower().Contains(searchString.ToLower()));
            
            paging.TotalItems = students.Count();
        }
        
        students =  students.OrderBy(p => p.Id)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize);
        

        if (paging.CurrentPage > paging.TotalPages)
        {
            paging.CurrentPage = 1;
        }
        
        var studentVm = new StudentVM
        {
            Students = students,
            SearchString = searchString,
            PagingInfo = paging
        };
        
        return studentVm;
    }
    
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        // ReSharper disable once PossibleLossOfFraction
        public int TotalPages => (int)(Math.Ceiling((double)TotalItems / ItemsPerPage));
    }

    private int PageSize = 4;
    public IActionResult List(string? searchString,int? productPage)
    {
        Console.WriteLine("request received");
        return View(GetStudentList(searchString, productPage ?? 1));
    }
    
    public PartialViewResult GetPartialStudentList(string? searchString,int? productPage)
    {

        var VM = GetStudentList(searchString, productPage ?? 1);
        Response.Headers.Add("X-Total-Count", VM.PagingInfo.TotalItems.ToString());
        
        return PartialView("StudentListPartial", VM);
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

        if (id.HasValue)
        {
            model = _repoStudent.Models
                .Where(model => model.Id == id)
                .Include(o => o.Class)
                .ThenInclude(o => o.Education)
                .Include(o => o.Class)
                .ThenInclude(o => o.ClassCourses)
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
    public IActionResult Edit(StudentEditVM model)
    {
        if (!ModelState.IsValid)
        {
            model.possibleEducations = _repoEdu.Models.Include(o => o.Courses);
                    
            return View(model);
        }
           
        
        if (model.student.Id != 0)
            _repoStudent.Update(model.student);
        else
            _repoStudent.Insert(model.student);
        
        return RedirectToAction("List");
    }
}