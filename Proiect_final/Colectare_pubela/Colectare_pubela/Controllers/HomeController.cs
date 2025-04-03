using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Colectare_pubela.Models;
using Colectare_pubela.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Colectare_pubela.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        VerificareEroriColectari();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public void VerificareEroriColectari()
    {
        var colectariDb = _context.Colectari.ToList();
        var pubeleDb = _context.Pubele.ToList();
        var pubeleCetateniDb = _context.PubeleCetateni.ToList();

        List<string> errorMessages = new List<string>();

        foreach (var colectare in colectariDb)
        {
            bool pubelaExista = pubeleDb.Any(pubela => pubela.TagId == colectare.TagId);
            var pubelaCorecta = pubeleCetateniDb.FirstOrDefault(pubela => pubela.TagId == colectare.TagId);
            bool adresaCorecta = false;

            if (pubelaCorecta != null)
            {
                if (pubelaCorecta.Address == colectare.Address)
                {
                    adresaCorecta = true;
                }
            }

            if (!pubelaExista)
            {
                errorMessages.Add($"Dumpster with Tag ID {colectare.TagId} (Entry {colectare.Id}) is not registered.");
            }
            else if (!adresaCorecta)
            {
                errorMessages.Add($"Dumpster with Tag ID {colectare.TagId} (Entry {colectare.Id}) was collected from an incorrect address.");
            }
        }

        if (errorMessages.Any())
        {
            if (errorMessages.Count == 1)
            {
                TempData["AlertMessage"] = errorMessages.First();
            }
            else
            {
                TempData["AlertMessage"] = "Multiple errors found in the collection history!";
            }
        }
    }
}
