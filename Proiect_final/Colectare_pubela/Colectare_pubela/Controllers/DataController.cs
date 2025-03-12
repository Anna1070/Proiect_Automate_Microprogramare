using Colectare_pubela.Data;
using Colectare_pubela.Models;
using Microsoft.AspNetCore.Mvc;

namespace Colectare_pubela.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : Controller
    {
        private readonly AppDbContext _context;

        public DataController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] DataStructure data)
        {
            if (data == null)
                return BadRequest("Invalid data.");

            _context.DataStructure.Add(data);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data received successfully!" });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
