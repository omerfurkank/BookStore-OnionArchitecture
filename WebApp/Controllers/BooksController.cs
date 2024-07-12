using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.Json;
using WebApp.Models.Book;

namespace WebApp.Controllers;
[Authorize]
public class BooksController : Controller
{
    private readonly IValidator<CreateBookModel> _validator;
    public BooksController(IValidator<CreateBookModel> validator)
    {
        _validator = validator;
    }
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

        return View(new CreateBookModel());
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
