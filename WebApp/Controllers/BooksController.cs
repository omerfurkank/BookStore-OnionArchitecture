using Application.Features.Books.Commands.CreateBook;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
public class BooksController : Controller
{
    private readonly IHttpClientFactory _clientFactory;
    public BooksController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    [HttpPost]
    public async Task<IActionResult> Add(CreateBookCommandRequest request)
    {
        var client = _clientFactory.CreateClient();
        client.BaseAddress = new Uri("http://localhost:5298");
        var response = await client.PostAsJsonAsync("api/Books/add", request);
        var createdBook = await response.Content.ReadFromJsonAsync<Book>();
        return View(createdBook);
    }
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return View();
    }
}
