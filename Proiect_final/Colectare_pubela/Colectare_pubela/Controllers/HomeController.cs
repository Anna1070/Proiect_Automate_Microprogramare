using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Colectare_pubela.Models;
using Colectare_pubela.Data;

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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
