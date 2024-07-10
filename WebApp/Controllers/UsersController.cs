using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
}
