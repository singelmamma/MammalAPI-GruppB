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

        public async Task<List<Mammal>> GetAllMammals()
        {
            _logger.LogInformation($"Getting all mammals");

            var query = _dBContext.Mammals;

            return await query.ToListAsync();
        }

        public async Task<MammalDTO> GetMammalById(int id)
        {
            _logger.LogInformation($"Getting mammal with {id}");

            var query = _dBContext.Mammals
                .Include(f => f.Family)
                .Include(mh => mh.MammalHabitats)
                .Where(x => x.MammalId == id)

                .Select(x => new MammalDTO
                {
                    MammalID = x.MammalId,
                    Name = x.Name,
                    Children = 0,
                    Length = x.Length,
                    Weight = x.Weight,
                    LatinName = x.LatinName,
                    Lifespan = x.Lifespan,
                    Habitats = x.MammalHabitats.Select(h => h.Habitat).ToList(),
                    Family = x.Family
                });

            if (query == null) throw new Exception($"Not found: { id }");
  
            return await query.SingleOrDefaultAsync();
        }

        public async Task<List<FamilyDTO>> GetMammalsByHabitat(string habitatName)
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
                .Select(s => new FamilyDTO
                {
                    Name = s.h.Name,
                    FamilyID = s.h.MammalId
                });

            return await query.ToListAsync();
        }

        public async Task<List<FamilyDTO>> GetMammalsByHabitatId(int id)
        {
            _logger.LogInformation($"Getting mammals in habitat by id: {id}");

            var query = _dBContext.MammalHabitats
                .Include(mh => mh.Mammal)
                .AsNoTracking()
                .Where(x => x.HabitatId == id)
                .Select(x => new FamilyDTO
                {
                    FamilyID = x.MammalId,
                    Name = x.Mammal.Name
                });

            return await query.ToListAsync();
        }

        public async Task<List<MammalLifespanDTO>> GetMammalsByLifeSpan(int fromYear, int toYear)
        {
            _logger.LogInformation($"Getting mammals by lifespan: {fromYear}-{toYear}");
            var query = _dBContext.Mammals
                .Where(x => x.Lifespan >= fromYear && x.Lifespan <= toYear)
                .Select(x => new MammalLifespanDTO
                {
                    Id = x.MammalId,
                    Name = x.Name,
                    Lifespan = x.Lifespan
                });



            return await query.ToListAsync(); 
        }

        public async Task<List<FamilyDTO>> GetMammalsByFamily(string familyName)
        {
            _logger.LogInformation($"Getting mammals by familyname {familyName}");

            var query = _dBContext.Mammals
                .Include(m => m.Family)
                .AsNoTracking()
                .Where(m => m.Family.Name == familyName)
                .Select(x => new FamilyDTO
                {
                    FamilyID = x.MammalId,
                    Name = x.Name
                });

            return await query.ToListAsync();
        }

        public async Task<List<FamilyDTO>> GetMammalsByFamilyId(int id)
        {
            _logger.LogInformation($"Getting mammals by familyid {id}");

            var query = _dBContext.Mammals
                .Where(m => m.Family.FamilyId == id)
                .Select(m => new FamilyDTO
                {
                   FamilyID = m.MammalId,
                    Name = m.Name
                });

            return await query.ToListAsync();
        }

    }
}
