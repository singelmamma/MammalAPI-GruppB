using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.DTO;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MammalAPI.Services
{
    public class HabitatRepository : IHabitatRepository
    {
        private readonly DBContext _dBContext;
        private readonly ILogger<HabitatRepository> _logger;
        public HabitatRepository(DBContext context, ILogger<HabitatRepository> logger)
        {
            _logger = logger;
            _dBContext = context;
        }

        public async Task<List<Habitat>> GetAllHabitats()
        {
            _logger.LogInformation("Getting all habitats");
            return await _dBContext.Habitats.ToListAsync();
        }

        public async Task<IdNameDTO> GetHabitatByName(string name)
        {
            _logger.LogInformation($"Getting habitat with name: { name }");
            var query = _dBContext.Habitats
                            .Where(h => h.Name == name)
                            .Select(s => new IdNameDTO
                            {
                                Name = s.Name,
                                Id = s.HabitatID
                            });

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Habitat> GetHabitatById(int id)
        {
            _logger.LogInformation($"Getting habitat with id: { id }");
            return await _dBContext.Habitats
                .FirstOrDefaultAsync(x => x.HabitatID == id);
        }
    }
}
