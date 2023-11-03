using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using StudentManager.Models.DBUtils;
using StudentManager.Utils;

namespace StudentManager.Controllers;

public class ClassController : Controller
{
    private readonly IModelRepository<ClassModel> _repoClass;

    public ClassController(IModelRepository<ClassModel> repoStudent)
    {
        _repoClass = repoStudent;
    }

    public IActionResult List()
    {
        return View();
    }
    
    // GET
    public List<ClassModel> Select(int? educationID)
    {
        var models = _repoClass.Models;
        
        if (educationID.IsNotNull(out var Id))
        {
            models = models.Where(o => Equals(o.EducationId, Id));
        }
        
        return models.ToList();
    }
    
    
    //crud operations
    public ClassModel? Get(int id)
    {
        return _repoClass.Models.FirstOrDefault(o => o.Id == id);
    }
    
    public ClassModel? Create(ClassModel model)
    {
        return _repoClass.Insert(model);
    }
    
    public void Update(ClassModel model)
    {
        _repoClass.Update(model);
        
        Response.StatusCode = 203;
    }
    
    public void Delete(ClassModel model)
    {
        _repoClass.Delete(model);
        
        Response.StatusCode = 203;
    }
}