using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Utils;

namespace StudentManager.Components;

public class FilterViewComponent : ViewComponent
{
    private IModelRepository<Student> repository;
    public FilterViewComponent(IModelRepository<Student> repo) {
        repository = repo;
    }
    
    public IViewComponentResult Invoke(filterData[] filters) {
        return View(filters);
    }
}