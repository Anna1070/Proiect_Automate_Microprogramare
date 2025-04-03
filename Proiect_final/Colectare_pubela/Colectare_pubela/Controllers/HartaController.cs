using Colectare_pubela.Data;
using Colectare_pubela.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace Colectare_pubela.Controllers
{
    public class HartaController : Controller
    {
        private readonly AppDbContext _context;
        public HartaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AfiseazaHarta()
        {
            DateTime dataColectarii = new DateTime(2024, 10, 15);
            ViewData["FormattedDate"] = dataColectarii.ToString("dd.MM.yyyy");

            var colectari = _context.Colectari.Where(c => c.CollectionTime.Date == dataColectarii.Date)
                                              .OrderBy(c => c.CollectionTime)
                                              .Select(c => new
                                              {
                                                  Latitudine = !string.IsNullOrEmpty(c.Latitudine) ? c.Latitudine : "0",
                                                  Longitudine = !string.IsNullOrEmpty(c.Longitudine) ? c.Longitudine : "0"
                                              })
                                              .ToList();
            ViewData["Colectari"] = System.Text.Json.JsonSerializer.Serialize(colectari);
            return View();
        }
    }
}
