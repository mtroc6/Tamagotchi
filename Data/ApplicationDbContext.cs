using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Tamagotchi.Data;

namespace Tamagotchi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Pets> Pets { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<CurrentTamagotchi> CurrentTamagotchis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pets>()
               .HasOne(p => p.Statistics)
               .WithOne()
               .HasForeignKey<Pets>(p => p.Id_Stat);
        }
    }
}