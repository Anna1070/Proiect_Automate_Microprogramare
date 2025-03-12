using Microsoft.EntityFrameworkCore;

namespace Colectare_pubela.Data
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
