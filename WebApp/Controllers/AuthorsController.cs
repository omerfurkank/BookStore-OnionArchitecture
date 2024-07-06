using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApp.Models.Author;

namespace WebApp.Controllers;
public class AuthorsController : Controller
{
    public IActionResult GetList()
    { 
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        return View();
    }
    public async Task<IActionResult> GetById(int id)
    {
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        return View(id);
    }
    public async Task<IActionResult> Create()
    {
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        return View(new CreateAuthorModel());
    }
    public async Task<IActionResult> Update(int id)
    {
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        return View(id);
    }
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        return View(id);
    }
}
