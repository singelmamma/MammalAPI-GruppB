using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.DTO;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MammalAPI.Services
{
    public class HabitatRepository : IHabitatRepository
    {
        private readonly DBContext _dBContext;
        public HabitatRepository(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<List<Habitat>> GetAllHabitats()
        {
            return await _dBContext.Habitats.ToListAsync();
        }

        public async Task<IdNameDTO> GetHabitatByName(string name)
        {
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
            return await _dBContext.Habitats
                .FirstOrDefaultAsync(x => x.HabitatID == id);
        }
    }
}
