using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MammalAPI.Services
{
    public class HabitatRepository : Repository, IHabitatRepository
    {
        public HabitatRepository(DBContext DBContext, ILogger<HabitatRepository> logger) : base(DBContext, logger)
        { }

        public async Task<List<Habitat>> GetAllHabitats(bool includeMammal=false)
        {
            _logger.LogInformation("Getting all habitats");
            IQueryable<Habitat> query = _dBContext.Habitats;

            if (includeMammal)
            {
                query = query.Include(x => x.MammalHabitats).ThenInclude(x => x.Mammal);
            }

            return await query.ToListAsync();
        }

        public async Task<Habitat> GetHabitatById(int id, bool includeMammal = false)
        {
            _logger.LogInformation($"Getting habitat with id: { id }");
            IQueryable<Habitat> query = _dBContext.Habitats.Where(x => x.HabitatID == id);
            
            if (includeMammal == true)
            {
                query = query.Include(x => x.MammalHabitats).ThenInclude(x=>x.Mammal);
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Habitat> GetHabitatByName(string name, bool includeMammal=false)
        {
            _logger.LogInformation($"Getting habitat with name: { name }");
            IQueryable<Habitat> query = _dBContext.Habitats.Where(c => c.Name == name); ;
            
            if (includeMammal==true)
            {
                query = query.Include(x => x.MammalHabitats).ThenInclude(x => x.Mammal);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
