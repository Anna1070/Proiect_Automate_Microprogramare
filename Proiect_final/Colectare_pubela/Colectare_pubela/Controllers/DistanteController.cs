using Colectare_pubela.Data;
using Microsoft.AspNetCore.Mvc;

namespace Colectare_pubela.Controllers
{
    public class DistanteController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DistanteController> _logger;

        public DistanteController(AppDbContext context, ILogger<DistanteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult CalculeazaDistante()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
