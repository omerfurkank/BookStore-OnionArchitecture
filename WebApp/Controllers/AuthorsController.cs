using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Controllers;
public class AuthorsController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        base.OnActionExecuting(context);
    }
    public async Task<IActionResult> GetList()
    { 
        return View();
    }
    public async Task<IActionResult> GetById(int id)
    {
        return View(id);
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    public async Task<IActionResult> Update(int id)
    {
        return View(id);
    }
    public async Task<IActionResult> Delete(int id)
    {
        return View(id);
    }
}
