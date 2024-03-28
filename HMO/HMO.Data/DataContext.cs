using AutoMapper.Configuration;
using HMO.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMO.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }


        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HMO_2DB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationship between members and vaccinations
            modelBuilder.Entity<Member>()
                .HasMany(m => m.Vaccinations)
                .WithOne(v => v.Member)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);
        }
    }
}
//זה כבר בעיה פנימית במחלקות