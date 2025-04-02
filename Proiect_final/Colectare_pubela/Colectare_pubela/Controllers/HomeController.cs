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
        return View();
    }

    public IActionResult InregistreazaColectare()
    {
        ViewBag.Pubele = new SelectList(_context.Pubele.ToList(), "TagId", "TagId");
        return View();
    }

    [HttpPost]
    public IActionResult InregistreazaColectare(Colectare colectare)
    {
        if (ModelState.IsValid)
        {
            var pubelaAsignata = _context.PubeleCetateni
             .FirstOrDefault(pc => pc.TagId == colectare.TagId);

            if (pubelaAsignata == null)
            {
                ModelState.AddModelError("Address", "Dumpster not assigned to any address.");
                ViewBag.Pubele = new SelectList(_context.Pubele.ToList(), "TagId", "TagId");
                return View(colectare);
            }

            if (pubelaAsignata.Address != colectare.Address)
            {
                var alerta = new
                {
                    TagId = colectare.TagId,
                    ColectareAddress = colectare.Address,
                    ContractAddress = pubelaAsignata.Address,
                    CollectionTime = colectare.CollectionTime
                };

                TempData["AlertMessage"] = $"Alert: Dumpster with Tag {alerta.TagId} was collected from {alerta.ColectareAddress} (expected: {alerta.ContractAddress}) at {alerta.CollectionTime}.";
            }

            _context.Colectari.Add(colectare);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Pubele = new SelectList(_context.Pubele.ToList(), "TagId", "TagId");
        return View(colectare);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
