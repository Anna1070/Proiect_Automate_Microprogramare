using ClosedXML.Excel;
using Colectare_pubela.Data;
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
                                                  Longitudine = !string.IsNullOrEmpty(c.Longitudine) ? c.Longitudine : "0",
                                                  c.Address
                                              })
                                              .ToList();
            ViewData["Colectari"] = System.Text.Json.JsonSerializer.Serialize(colectari);

            string solutionDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            string filePath = Path.Combine(solutionDirectory, "Traseu_2_SB_77_ULB.xlsx");
            var matriceaDistantelor = CitesteMatriceaDistantelor(filePath);
            var rutaOptimizata = GasesteRutaOptimizata(matriceaDistantelor);
            ViewData["RutaOptimizata"] = System.Text.Json.JsonSerializer.Serialize(rutaOptimizata);

            return View();
        }

        public double[,] CitesteMatriceaDistantelor(string pathFisier)
        {
            using var workbook = new XLWorkbook(pathFisier);
            var worksheet = workbook.Worksheet("Matrix");

            int nrPuncte = worksheet.LastRowUsed().RowNumber() - 1;
            double[,] matrice = new double[nrPuncte, nrPuncte];

            for (int i = 0; i < nrPuncte; i++)
            {
                for (int j = 0; j < nrPuncte; j++)
                {
                    var cellValue = worksheet.Cell(i + 2, j + 2).GetValue<string>().Replace(',', '.');

                    if (double.TryParse(cellValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double distanta))
                    {
                        matrice[i, j] = distanta;
                    } else
                    {
                        matrice[i, j] = double.MaxValue;
                    }
                }
            }
            return matrice;
        }

        public List<int> GasesteRutaOptimizata(double[,] matriceaDistantelor)
        {
            int n = matriceaDistantelor.GetLength(0);
            bool[] vizitat = new bool[n];
            List<int> ruta = new List<int>();

            double minDistantaStart = double.MaxValue;
            int primulPunct = -1;

            for (int i = 0; i < n; i++)
            {
                if (matriceaDistantelor[0, i] < minDistantaStart) 
                {
                    minDistantaStart = matriceaDistantelor[0, i];
                    primulPunct = i;
                }
            }

            ruta.Add(primulPunct);
            vizitat[primulPunct] = true;
            int punctCurent = primulPunct;

            while (ruta.Count < n)
            {
                double minDistanta = double.MaxValue;
                int urmatorulPunct = -1;

                for (int i = 0; i < n; i++)
                {
                    if (!vizitat[i] && matriceaDistantelor[punctCurent, i] < minDistanta)
                    {
                        minDistanta = matriceaDistantelor[punctCurent, i];
                        urmatorulPunct = i;
                    }
                }

                if (urmatorulPunct == -1)
                    break;

                ruta.Add(urmatorulPunct);
                vizitat[urmatorulPunct] = true;
                punctCurent = urmatorulPunct;
            }

            return ruta;
        }
    }
}
