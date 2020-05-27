using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.DTO;
using MammalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MammalAPI.Services
{
    public class HabitatRepository : Repository, IHabitatRepository
    {
        public HabitatRepository(DBContext DBContext, ILogger<FamilyRepository> logger) : base(DBContext, logger)
        { }

        public async Task<List<Habitat>> GetAllHabitats()
        {
            _logger.LogInformation("Getting all habitats");
            return await _dBContext.Habitats.ToListAsync();
        }

        public async Task<Habitat> GetHabitatByName(string name, bool includeMammal=false)
        {
            _logger.LogInformation($"Getting habitat with name: { name }");
            IQueryable<Habitat> query = _dBContext.Habitats;
            
            if (includeMammal==true)
            {
                query = query.Include(x => x.MammalHabitats).ThenInclude(x => x.Mammal);
            }

            query = query.Where(c => c.Name == name);
            if (query == null) throw new System.Exception($"Not found {name}");
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Habitat> GetHabitatById(int id)
        {
            _logger.LogInformation($"Getting habitat with id: { id }");
            var query = _dBContext.Habitats.Where(x => x.HabitatID == id).FirstOrDefaultAsync();

            if (query == null) throw new System.Exception($"Not found {id}");
            return await query;
        }
    }
}
