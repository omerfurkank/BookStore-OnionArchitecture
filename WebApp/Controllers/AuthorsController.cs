using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Controllers;
public class AuthorsController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewData["Token"] = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        base.OnActionExecuting(context);
    }

    public IActionResult ExcelDownload()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Authors");

            // Başlık satırı
            worksheet.Cell(1, 1).Value = "Name";

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;

                // Kullanıcıya indirme olarak sunma
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Authors.xlsx");
            }
        }
    }
    public IActionResult ExcelUpload()
    {
        return View();
    }
    public class AuthorModel
    {
        public string Name { get; set; }
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
        return View();
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
