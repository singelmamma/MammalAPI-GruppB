using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MammalAPI.Context
{
    public class DBContext: DbContext
    {
        public DbSet<FakeMammal> FakeMammal { get; set; }

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
            modelBuilder.ApplyConfiguration(new FakeConfiguration());   //creating a FakeMammal table
        }
    }
}
