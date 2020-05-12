using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<Habitat> GetHabitatById(int id)
        {
            return await _dBContext.Habitats
                .FirstOrDefaultAsync(x => x.HabitatID == id);
        }
    }
}
