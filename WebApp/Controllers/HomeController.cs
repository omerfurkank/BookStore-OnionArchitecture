using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
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
