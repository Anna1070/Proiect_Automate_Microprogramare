using Colectare_pubela.Data;
using Colectare_pubela.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Colectare_pubela.Controllers
{
    public class ColectariController : Controller
    {
        private readonly AppDbContext _context;

        public ColectariController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult IstoricColectari()
        {
            var colectariDb = _context.Colectari.ToList();
            var pubeleDb = _context.Pubele.ToList();
            var pubeleCetateniDb = _context.PubeleCetateni.ToList();

            List<ColectareViewModel> colectari = new List<ColectareViewModel>();

            foreach (var colectare in colectariDb)
            {
                bool pubelaExista = pubeleDb.Any(pubela => pubela.TagId == colectare.TagId);
                var pubelaCorecta = pubeleCetateniDb.FirstOrDefault(pubela => pubela.TagId == colectare.TagId);
                bool adresaCorecta = false;
                if(pubelaCorecta != null)
                {
                   if(pubelaCorecta.Address == colectare.Address)
                   {
                        adresaCorecta=true;
                   }
                }

                bool hasError = false;
                string errorType = "";

                if (!pubelaExista)
                {
                    hasError = true;
                    errorType = "TagId";
                }
                else if (!adresaCorecta)
                {
                    hasError = true;
                    errorType = "Address";
                }

                colectari.Add(new ColectareViewModel
                {
                    Id = colectare.Id,
                    TagId = colectare.TagId,
                    CollectionTime = colectare.CollectionTime,
                    Address = colectare.Address,
                    HasError = hasError,
                    ErrorType = errorType
                });
            }

            return View(colectari);
        }
    }
}
