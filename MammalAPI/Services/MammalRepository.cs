using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MammalAPI.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace MammalAPI.Services
{
    public class MammalRepository : Repository, IMammalRepository
    {
        public MammalRepository(DBContext DBContext, ILogger<MammalRepository> logger): base(DBContext, logger)
        {}

        public async Task<List<Mammal>> GetAllMammals()
        {
            _logger.LogInformation($"Getting all mammals");

            return await _dBContext.Mammals.ToListAsync();
        }

        public async Task<Mammal> GetMammalById(int id)
        {
            _logger.LogInformation($"Getting mammal with {id}");

            return await _dBContext.Mammals
                .FirstOrDefaultAsync(m => m.MammalId == id);
        }

        public async Task<List<IdNameDTO>> GetMammalsByHabitat(string habitatName)
        {
            _logger.LogInformation($"Getting mammals in habitat by name: {habitatName}");

            var query = _dBContext.MammalHabitats
                .Join(_dBContext.Habitats
                .Where(h => h.Name == habitatName),
                mh => mh.HabitatId,
                h => h.HabitatID,
                (mh, h) => new { mh, h })
                .Join(_dBContext.Mammals,
                m => m.mh.MammalId,
                n => n.MammalId,
                (m, h) => new { m, h })
                .Select(s => new IdNameDTO
                {
                    Name = s.h.Name,
                    Id = s.h.MammalId
                });

            return await query.ToListAsync();
        }

        public async Task<List<IdNameDTO>> GetMammalsByHabitatId(int id)
        {
            _logger.LogInformation($"Getting mammals in habitat by id: {id}");

            var query = _dBContext.MammalHabitats
                .Include(mh => mh.Mammal)
                .AsNoTracking()
                .Where(x => x.HabitatId == id)
                .Select(x => new IdNameDTO
                {
                    Id = x.MammalId,
                    Name = x.Mammal.Name
                });

            return await query.ToListAsync();
        }

        public async Task<Mammal> GetMammalByLifeSpan(int lifespan)
        {
            _logger.LogInformation($"Getting mammals by lifespan: {lifespan}");

            return await _dBContext.Mammals
                .FirstOrDefaultAsync(m => m.Lifespan == lifespan);
        }

    }
}
