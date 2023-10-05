using Microsoft.AspNetCore.Mvc;

namespace StudentManager.Controllers;

public class ClassController : Controller
{
    public IActionResult List()
    {
        return View();
    }
}