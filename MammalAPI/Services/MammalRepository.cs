using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MammalAPI.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;

namespace MammalAPI.Services
{
    public class MammalRepository : Repository, IMammalRepository
    {
        public MammalRepository(DBContext DBContext, ILogger<MammalRepository> logger): base(DBContext, logger)
        {}

        public async Task<List<MammalsDTO>> GetAllMammals()
        {
            _logger.LogInformation($"Getting all mammals");

            var query = _dBContext.Mammals
                .Select(m => new MammalsDTO
                {
                    MammalId = m.MammalId,
                    Name = m.Name,
                    LatinName = m.LatinName,
                    Length = m.Length,
                    Weight = m.Weight
                });

            return await query.ToListAsync();
        }

        public async Task<Mammal> GetMammalById(int id)
        {
            _logger.LogInformation($"Getting mammal with {id}");
                var result = await _dBContext.Mammals
                    .FirstOrDefaultAsync(m => m.MammalId == id);

            if (result == null) throw new Exception($"Not found: { id }");

            return result;
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

        public async Task<List<IdNameDTO>> GetMammalsByLifeSpan(int fromLifespanYear, int toLifespanYear)
        {
            _logger.LogInformation($"Getting mammals by lifespan: {fromLifespanYear}-{toLifespanYear}");
            var query = _dBContext.Mammals
                .Where(x => x.Lifespan >= fromLifespanYear && x.Lifespan <= toLifespanYear)
                .Select(x => new IdNameDTO
                {
                    Id = x.MammalId,
                    Name = x.Name
                });



            return await query.ToListAsync(); 
        }

        public async Task<List<IdNameDTO>> GetMammalsByFamily(string familyName)
        {
            _logger.LogInformation($"Getting mammals by familyname {familyName}");

            var query = _dBContext.Mammals
                .Include(m => m.Family)
                .AsNoTracking()
                .Where(m => m.Family.Name == familyName)
                .Select(x => new IdNameDTO
                {
                    Id = x.MammalId,
                    Name = x.Name
                });

            return await query.ToListAsync();
        }

        public async Task<List<IdNameDTO>> GetMammalsByFamilyId(int id)
        {
            _logger.LogInformation($"Getting mammals by familyid {id}");

            var query = _dBContext.Mammals
                .Where(m => m.Family.FamilyId == id)
                .Select(m => new IdNameDTO
                {
                    Id = m.MammalId,
                    Name = m.Name
                });

            return await query.ToListAsync();
        }

    }
}
