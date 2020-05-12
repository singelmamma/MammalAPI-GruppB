using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Context;
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
    }
}
