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

    private int PageSize = 4;

    public StudentController(IModelRepository<Student> repoStudent)
    {
        _repoStudent = repoStudent;
    }


    public class SearchParameters
    {
        public string? SearchString { get; set; }
        public int? EducationID { get; set; }
        
        public int? classID { get; set; }
        public int Page { get; set; } = 1;

        public bool ParametersToBeApplied()
        {
            return !string.IsNullOrEmpty(SearchString) || (EducationID ?? classID) != null;
        }
    }   
    
    public StudentVM GetStudentList(SearchParameters parameters)
    {
        var paging = new PagingInfo
        {
            CurrentPage = parameters.Page,
            ItemsPerPage = PageSize,
            TotalItems = parameters.ParametersToBeApplied() ? 0 : _repoStudent.Models.Count()
        };

        var students = (IQueryable<Student>)_repoStudent.Models
            .Include(o => o.Class)
            .ThenInclude(o => o.Education);

        if (parameters.ParametersToBeApplied())
        {
            if (!string.IsNullOrEmpty(parameters.SearchString))
            {
                students = students.Where(s => s.FirstName.ToLower().Contains(parameters.SearchString.ToLower())
                                               || s.LastName.ToLower().Contains(parameters.SearchString.ToLower()));
            }
            
            if (parameters.classID != null)
            {
                students = students.Where(s => Equals(s.ClassId,parameters.classID));
            }
            
            if (parameters.EducationID != null)
            {
                students = students.Where(s => s.Class != null && Equals(s.Class.Education.Id,parameters.EducationID));
            }
            
            paging.TotalItems = students.Count();
        }
        
        if (paging.CurrentPage > paging.TotalPages)
            paging.CurrentPage = 1;
        
        students = students.OrderBy(p => p.Id)
            .Skip((parameters.Page - 1) * PageSize)
            .Take(PageSize);
        

        var studentVm = new StudentVM
        {
            Students = students,
            SearchString = parameters.SearchString,
            EducationID = parameters.EducationID,
            ClassID = parameters.classID,
            PagingInfo = paging
        };

        return studentVm;
    }

    public IActionResult List(SearchParameters parameters)
    {
        Console.WriteLine("request received");
        return View(GetStudentList(parameters));
    }

    public PartialViewResult GetPartialStudentList(SearchParameters parameters)
    {
        var vm = GetStudentList(parameters);
        Response.Headers.Add("X-Total-Count", vm.PagingInfo.TotalItems.ToString());

        return PartialView("StudentListPartial", vm);
    }


    [HttpGet]
    public RedirectToActionResult create()
    {
        var newStudent = new Student();

        //_repo.Insert(newStudent);

        return RedirectToAction("Edit", new { model = newStudent });
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
            model = _repoStudent.Models.Include(e=>e.Class)
                .FirstOrDefault(student => student.Id == id);
        }

        model ??= new Student();

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (!ModelState.IsValid)
        {
            return View(student);
        }


        if (student.Id != 0)
            _repoStudent.Update(student);
        else
            _repoStudent.Insert(student);

        return RedirectToAction("List");
    }

    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        // ReSharper disable once PossibleLossOfFraction
        public int TotalPages => (int)(Math.Ceiling((double)TotalItems / ItemsPerPage));
    }
}