//using Colectare_pubela.Data;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json.Linq;
//using OfficeOpenXml;
//using System.ComponentModel;

//namespace Colectare_pubela.Controllers
//{
//    public class DistanteController : Controller
//    {
//        private readonly AppDbContext _context;

//        public DistanteController(AppDbContext context)
//        {
//            _context = context;
//            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
//        }

//        public async Task<IActionResult> CalculeazaDistante()
//        {
//            string token = "something";

//            var puncte = _context.Colectari
//                .Select(c => new { c.Latitudine, c.Longitudine })
//                .ToList();

//            double startLat = 45.7315361;
//            double startLon = 24.1779393;
//            double stopLat = 45.7877059;
//            double stopLon = 24.0247875;

//            List<string> toateCoordonate = new List<string>();

//            toateCoordonate.Add($"{startLon.ToString(System.Globalization.CultureInfo.InvariantCulture)},{startLat.ToString(System.Globalization.CultureInfo.InvariantCulture)}");

//            for (int i = 0; i < puncte.Count; i++)
//            {
//                double lat = double.Parse(puncte[i].Latitudine);
//                double lon = double.Parse(puncte[i].Longitudine);

//                string coord = $"{lon.ToString(System.Globalization.CultureInfo.InvariantCulture)},{lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
//                toateCoordonate.Add(coord);
//            }

//            toateCoordonate.Add($"{stopLon.ToString(System.Globalization.CultureInfo.InvariantCulture)},{stopLat.ToString(System.Globalization.CultureInfo.InvariantCulture)}");

//            List<List<double>> matriceFinala = new List<List<double>>();

//            for (int i = 0; i < toateCoordonate.Count; i++)
//            {
//                matriceFinala.Add(new List<double>(new double[toateCoordonate.Count]));
//            }

//            HttpClient client = new HttpClient();
//            int N = toateCoordonate.Count;
//            int batchSize = 10;
//            for (int i = 0; i < N; i += batchSize)
//            {
//                int endI = Math.Min(i + batchSize, N);
//                for (int j = 0; j < N; j += batchSize)
//                {
//                    int endJ = Math.Min(j + batchSize, N);

//                    var surse = toateCoordonate.GetRange(i, endI - i);
//                    var destinatii = toateCoordonate.GetRange(j, endJ - j);

//                    var coordonate = surse.Concat(destinatii).ToList();

//                    if (coordonate.Count > 25) continue;

//                    string urlCoords = string.Join(";", coordonate);
//                    string url = $"{urlCoords}?access_token={token}&sources={string.Join(";", Enumerable.Range(0, surse.Count))}&destinations={string.Join(";", Enumerable.Range(surse.Count, destinatii.Count))}&annotations=distance";

//                    var response = await client.GetAsync(url);
//                    var json = await response.Content.ReadAsStringAsync();

//                    var root = JObject.Parse(json);
//                    if (root["distances"] is JArray distances)
//                    {
//                        for (int x = 0; x < distances.Count; x++)
//                        {
//                            var linie = (JArray)distances[x];
//                            for (int y = 0; y < linie.Count; y++)
//                            {
//                                double dist = linie[y].Type == JTokenType.Null ? 0.0 : linie[y].ToObject<double>();
//                                matriceFinala[i + x][j + y] = dist;
//                            }
//                        }
//                    }
//                }
//            }

//            for (int i = 0; i < N; i++)
//                matriceFinala[i][i] = 0;

//            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())!
//                                                .Parent!
//                                                .Parent!
//                                                .FullName;
//            string folder = Path.Combine(projectRoot, "output");
//            Directory.CreateDirectory(folder);
//            string filePath = Path.Combine(folder, "matrice_distante.xlsx");

//            using (var package = new ExcelPackage())
//            {
//                var sheet = package.Workbook.Worksheets.Add("Distante");

//                for (int j = 0; j < N; j++)
//                    sheet.Cells[1, j + 2].Value = $"Punct {j}";
//                for (int i = 0; i < N; i++)
//                    sheet.Cells[i + 2, 1].Value = $"Punct {i}";

//                for (int i = 0; i < N; i++)
//                    for (int j = 0; j < N; j++)
//                        sheet.Cells[i + 2, j + 2].Value = matriceFinala[i][j];

//                package.SaveAs(new FileInfo(filePath));
//            }

//            string txtPath = Path.Combine(folder, "matrice_distante.txt");
//            using (StreamWriter writer = new StreamWriter(txtPath))
//            {
//                foreach (var linie in matriceFinala)
//                {
//                    string linieText = string.Join(",", linie.Select(d => d.ToString(System.Globalization.CultureInfo.InvariantCulture)));
//                    writer.WriteLine(linieText);
//                }
//            }

//            TempData["AlertMessage"] = "Matricea de distanțe a fost generată și salvată în Excel.";
//            return RedirectToAction("Index", "Home");
//        }
//    }
//}
