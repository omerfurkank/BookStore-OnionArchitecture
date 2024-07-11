using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Models.User;

namespace WebApp.Controllers;
public class UsersController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UsersController(IHttpContextAccessor httpContextAccessor)
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
    public async Task<IActionResult> UpdateUserWithRoles(int id)
    {
        return View(id);
    }
}
