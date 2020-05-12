using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MammalAPI.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MammalAPI.Services
{
    public class MammalRepository : IMammalRepository
    {
        private readonly DBContext _dBContext;

        public MammalRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<List<Mammal>> GetAllMammals()
        {
           
           return await _dBContext.Mammals.ToListAsync();
        }

        public async Task<Mammal> GetMammalById(int id)
        {
            return await _dBContext.Mammals
                .FirstOrDefaultAsync(m => m.MammalId == id);
        }

        public async Task<List<IdNameDTO>> GetMammalsByHabitat(string habitatName)
        {
            var query = _dBContext.MammalHabitats
                .Join(_dBContext.Habitats
                .Where(h => h.Name == habitatName),
                mh => mh.HabitatId,
                h => h.HabitatID,
                (mh, h) => new {mh, h})
                .Join(_dBContext.Mammals,
                m => m.mh.MammalId,
                n => n.MammalId,
                (m, h) => new {m, h})
                .Select(s => new IdNameDTO
                {
                    Name = s.h.Name,
                    Id = s.h.MammalId
                });
            
            return await query.ToListAsync();
        }
    }
}
