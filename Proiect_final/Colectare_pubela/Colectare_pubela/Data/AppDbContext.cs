using Microsoft.EntityFrameworkCore;
using Colectare_pubela.Models;

namespace Colectare_pubela.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Colectare> Colectari { get; set;  }
        public DbSet<Cetatean> Cetateni { get; set; }
        public DbSet<PubeleCetateni> PubeleCetateni { get; set; }
        public DbSet<Pubela> Pubele { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colectare>()
                .Property(d => d.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<PubeleCetateni>()
               .HasOne(pc => pc.Pubela)
               .WithMany()
               .HasForeignKey(pc => pc.TagId);

            modelBuilder.Entity<PubeleCetateni>()
                .HasOne(pc => pc.Cetatean)
                .WithMany()
                .HasForeignKey(pc => pc.IdCetatean);

            modelBuilder.Entity<PubeleCetateni>()
                .HasOne(pc => pc.Cetatean)
                .WithMany()
                .HasForeignKey(pc => pc.IdCetatean);
        }
    }
}
