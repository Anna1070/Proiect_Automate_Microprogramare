using Colectare_pubela.Data;
using Colectare_pubela.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Colectare_pubela.Controllers
{
    public class PubeleController : Controller
    {
        private readonly AppDbContext _context;

        public PubeleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AdaugaPubela()
        {
            return View();
        }

        public IActionResult AtribuirePubela()
        {
            ViewBag.Cetateni = new SelectList(_context.Cetateni
                .Select(c => new { c.Id, Fullname = c.Name + " " + c.Surname })
                .ToList(), "Id", "Fullname");
            ViewBag.Pubele = new SelectList(_context.Pubele
                .Where(p => !_context.PubeleCetateni.Any(pc => pc.TagId == p.TagId))
                .ToList(), "TagId", "TagId");

            return View();
        }

        [HttpPost]
        public IActionResult AdaugaPubela(Pubela pubela)
        {
            if (ModelState.IsValid)
            {
                _context.Pubele.Add(pubela);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
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

                    ViewBag.Pubele = new SelectList(_context.Pubele
                        .Where(p => !_context.PubeleCetateni.Any(pc => pc.TagId == p.TagId))
                        .ToList(), "TagId", "TagId");

                    return View(model);
                }

                _context.PubeleCetateni.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Cetateni = new SelectList(_context.Cetateni
               .Select(c => new { c.Id, Fullname = c.Name + " " + c.Surname })
               .ToList(), "Id", "Fullname");

            ViewBag.Pubele = new SelectList(_context.Pubele
                .Where(p => !_context.PubeleCetateni.Any(pc => pc.TagId == p.TagId))
                .ToList(), "TagId", "TagId");

            return View(model);
        }
    }
}
