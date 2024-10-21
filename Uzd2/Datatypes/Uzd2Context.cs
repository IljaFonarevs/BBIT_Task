using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Uzd2.Datatypes
{
    public class Uzd2Context : IdentityDbContext<ApplicationUser>
    {
        public Uzd2Context(DbContextOptions<Uzd2Context> options)
            : base(options) 
        { 
        }

        public DbSet<Maja> MajaItems { get; set; } = null!;
        public DbSet<Dzivoklis> DzivoklisItems { get; set; } = null!;
        public DbSet<Iedzivotajs> IedzivotajsItems { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DzivoklisIedzivotajs>()
                .HasKey(di => new { di.DzivoklisNumurs, di.IedzivotajsKods });

            modelBuilder.Entity<DzivoklisIedzivotajs>()
                .HasOne(di => di.Iedzivotajs)
                .WithMany(i => i.DzivIedz)
                .HasForeignKey(di => di.IedzivotajsKods);

            modelBuilder.Entity<DzivoklisIedzivotajs>()
                .HasOne(di => di.Dzivoklis)
                .WithMany(d => d.DzivIedz)
                .HasForeignKey(di => di.DzivoklisNumurs);

            modelBuilder.Entity<Maja>()
                .HasMany(m => m.Dzivoklis)
                .WithOne(d => d.Maja);
            base.OnModelCreating(modelBuilder);
        }
    }
}
