using Microsoft.EntityFrameworkCore;
using Colectare_pubela.Models;

namespace Colectare_pubela.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<DataStructure> DataStructure { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataStructure>()
                .Property(d => d.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
