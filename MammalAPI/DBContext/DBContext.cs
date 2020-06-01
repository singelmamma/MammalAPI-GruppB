using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MammalAPI.Context
{
    public class DBContext: DbContext
    {
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Habitat> Habitats { get; set; }
        public virtual DbSet<Mammal> Mammals { get; set; }
        public virtual DbSet<MammalHabitat> MammalHabitats { get; set; }

        public DBContext()
        {}
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var dbConnection = config.GetConnectionString("DefaultConnection");
            optionsbuilder.UseSqlServer(dbConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MammalHabitat>().HasKey(sc => new { sc.HabitatId, sc.MammalId });
            modelBuilder.Entity<Mammal>().HasOne(m => m.Family).WithMany(f => f.Mammals);
        }
    }
}
