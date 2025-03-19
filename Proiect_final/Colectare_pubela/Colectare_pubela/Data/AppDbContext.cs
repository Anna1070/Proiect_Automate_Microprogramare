using Microsoft.EntityFrameworkCore;
using Colectare_pubela.Models;

namespace Colectare_pubela.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Colectari> Colectari { get; set;  }
        public DbSet<Cetateni> Cetateni { get; set; }
        public DbSet<PubeleCetateni> PubeleCetateni { get; set; }
        public DbSet<Pubela> Pubela { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colectari>()
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
        }
    }
}
