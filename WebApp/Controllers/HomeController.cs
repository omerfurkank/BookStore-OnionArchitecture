using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;
[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [Authorize(Roles ="admin")]
    public string AdminPage()
    {
        return "admin page";
    }
    [Authorize(Roles = "user")]
    public string UserPage()
    {
        return "user page";
    }
}
