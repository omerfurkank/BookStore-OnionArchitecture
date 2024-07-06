using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApp.Models.Book;

namespace WebApp.Controllers;
[Authorize]
public class BooksController : Controller
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _client;
    public BooksController(IHttpClientFactory clientFactory, HttpClient client)
    {
        _clientFactory = clientFactory;
        _client = _clientFactory.CreateClient();
    }
    private void Auth()
    {
        var token = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
    public async Task<IActionResult> GetList()
    {
        Auth();
        var response = await _client.GetAsync("http://localhost:5298/api/Books/GetList");
        var jsonData = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<GetListBookModel>>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy= JsonNamingPolicy.CamelCase });
        return View(result);
    }
    public async Task<IActionResult> GetById(int id)
    {
        Auth();
        var response = await _client.GetAsync($"http://localhost:5298/api/Books/{id}");
        var jsonData = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GetBookModel>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        return View(result);
    }
    public async Task<IActionResult> Create()
    {

        return View(new CreateBookModel());
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookModel createBookModel)
    {
        Auth();
        var content = new StringContent(JsonSerializer.Serialize(createBookModel), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("http://localhost:5298/api/Books/Add", content);
        return Json(new { success = true, message = "Kitap başarıyla eklendi!" });
    }
    public async Task<IActionResult> Update(int id)
    {
        Auth();
        var response = await _client.GetAsync($"http://localhost:5298/api/Books/{id}");
        var jsonData = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<UpdateBookModel>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update([FromBody]UpdateBookModel updateBookModel)
    {
        Auth();
        var content = new StringContent(JsonSerializer.Serialize(updateBookModel), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync("http://localhost:5298/api/Books/Update",content);
        return Json(new { success = true, message = "Kitap başarıyla güncellendi!" });
    }
    public async Task<IActionResult> Delete(int id)
    {
        Auth();
        var response = await _client.DeleteAsync($"http://localhost:5298/api/Books/{id}");
        return RedirectToAction("GetList");
    }
}
