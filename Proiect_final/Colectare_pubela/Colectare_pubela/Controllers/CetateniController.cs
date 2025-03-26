using Colectare_pubela.Data;
using Colectare_pubela.Models;
using Colectare_pubela.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Colectare_pubela.Controllers
{
    public class CetateniController : Controller
    {
        private readonly AppDbContext _context;
        public CetateniController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AdaugaCetatean()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdaugaCetatean(Cetatean cetatean)
        {
            if (ModelState.IsValid)
            {
                _context.Cetateni.Add(cetatean);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(cetatean);
        }

        public IActionResult ColectariCetatean()
        {
            var cetateni = _context.Cetateni.ToList();
            var colectariCetateni = cetateni.Select(cetatean => new ColectariCetateanViewModel
            {
                CitizenId = cetatean.Id,
                CitizenName = cetatean.Name,
                CitizenSurname = cetatean.Surname,
                Colectari = _context.PubeleCetateni
                .Where(pc => pc.IdCetatean == cetatean.Id)
                .Join(
                    _context.Colectari,
                    pc => pc.TagId,
                    c => c.TagId,
                    (pc, c) => new ColectareViewModel
                    {
                        TagId = c.TagId,
                        CollectionTime = c.CollectionTime,
                        Address = c.Address
                    })
                .ToList()
            }).ToList();
            return View(colectariCetateni);
        }
    }
}
