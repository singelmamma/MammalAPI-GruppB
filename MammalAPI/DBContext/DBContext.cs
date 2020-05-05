using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MammalAPI.Context
{
    public class DBContext: DbContext
    {
        public DbSet<FakeMammal> FakeMammal { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Habitat> Habitats { get; set; }
        public DbSet<Mammal> Mammals { get; set; }
        public DbSet<MammalHabitat> MammalHabitats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var dbConnection = config.GetConnectionString("DefaultConnection");
            optionsbuilder.UseSqlServer(dbConnection);
        }

        //create db model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MammalHabitat>().HasKey(sc => new { sc.HabitatId, sc.MammalId });
            
            modelBuilder.ApplyConfiguration(new FakeConfiguration());   //creating a FakeMammal table
        }
    }
}
