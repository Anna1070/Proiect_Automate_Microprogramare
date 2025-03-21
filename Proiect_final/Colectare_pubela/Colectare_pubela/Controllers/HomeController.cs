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

    public IActionResult AdaugaCetatean()
    {
        return View();
    }
    public IActionResult AdaugaPubela()
    {
        return View();
    }

    public IActionResult AtribuirePubela()
    {
        ViewBag.Cetateni = new SelectList(_context.Cetateni
            .Select(c => new { c.Id, Fullname = c.Name + " " + c.Surname})
            .ToList(), "Id", "Fullname");
        ViewBag.Pubele = new SelectList(_context.Pubela
            .Where(p => !_context.PubeleCetateni.Any(pc => pc.TagId == p.TagId))
            .ToList(),"TagId", "TagId");

        return View();
    }

    [HttpPost]
    public IActionResult AdaugaCetatean(Cetateni cetatean)
    {
        if (ModelState.IsValid)
        {
            _context.Cetateni.Add(cetatean);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(cetatean);
    }

    [HttpPost]
    public IActionResult AdaugaPubela (Pubela pubela)
    {
        if (ModelState.IsValid)
        {
            _context.Pubela.Add(pubela);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(pubela);
    }

    [HttpPost]
    public IActionResult AtribuirePubela(PubeleCetateni model)
    {
        if (ModelState.IsValid)
        {
            var existaPubelaLaAdresa = _context.PubeleCetateni
                .Any(pc => pc.Address == model.Address);

            if (existaPubelaLaAdresa)
            {
                ModelState.AddModelError("Address", "There is already a dumpster assigned to this address");

                ViewBag.Cetateni = new SelectList(_context.Cetateni
                    .Select(c => new { c.Id, Fullname = c.Name + " " + c.Surname })
                    .ToList(), "Id", "Fullname");

                ViewBag.Pubele = new SelectList(_context.Pubela
                    .Where(p => !_context.PubeleCetateni.Any(pc => pc.TagId == p.TagId))
                    .ToList(), "TagId", "TagId");

                return View(model);
            }

            _context.PubeleCetateni.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Cetateni = new SelectList(_context.Cetateni
       .Select(c => new { c.Id, Fullname = c.Name + " " + c.Surname })
       .ToList(), "Id", "Fullname");

        ViewBag.Pubele = new SelectList(_context.Pubela
            .Where(p => !_context.PubeleCetateni.Any(pc => pc.TagId == p.TagId))
            .ToList(), "TagId", "TagId");

        return View(model);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
