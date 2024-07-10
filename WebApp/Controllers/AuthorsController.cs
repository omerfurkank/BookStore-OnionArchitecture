using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApp.Models.Author;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Controllers;
public class AuthorsController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthorsController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewData["Token"] = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
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
        return View(new CreateAuthorModel());
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
