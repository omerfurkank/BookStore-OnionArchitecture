using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;
public class HomeController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        base.OnActionExecuting(context);
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Add()
    {
        return View();
    }

    public string AdminPage()
    {
        return "admin page";
    }
    public string UserPage()
    {
        return "user page";
    }
}
