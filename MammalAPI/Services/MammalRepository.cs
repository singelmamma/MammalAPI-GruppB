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
using System.Security.Cryptography.X509Certificates;

namespace MammalAPI.Services
{
    public class MammalRepository : Repository, IMammalRepository
    {
        public MammalRepository(DBContext DBContext, ILogger<MammalRepository> logger): base(DBContext, logger)
        {}

        public async Task<List<Mammal>> GetAllMammals(bool includeFamily, bool includeHabitat)
        {
            _logger.LogInformation($"Getting all mammals");
            IQueryable<Mammal> query = _dBContext.Mammals;
            if (includeFamily)
            {
               query = query.Include(f => f.Family);
            }
            if (includeHabitat)
            {
                query = query.Include(mh => mh.MammalHabitats).ThenInclude(h => h.Habitat);
            }

            return await query.ToListAsync();
        }

        public async Task<Mammal> GetMammalById(int id)
        {
            _logger.LogInformation($"Getting mammal with {id}");

            var query = _dBContext.Mammals
                .Include(f => f.Family)
                .Include(mh => mh.MammalHabitats)
                .Where(x => x.MammalId == id);

            if (query == null) throw new Exception($"Not found: { id }");
  
            return await query.SingleOrDefaultAsync();
        }

        public async Task<List<Mammal>> GetMammalsByHabitat(string habitatName)
        {
            _logger.LogInformation($"Getting mammals in habitat by name: {habitatName}");


            IQueryable<Mammal> query = _dBContext.Mammals
                .Where(x => x.MammalHabitats.Any(z => z.Habitat.Name == habitatName));

            if (query == null) throw new Exception($"Not found: { habitatName }");


            return await query.ToListAsync();
        }

        public async Task<List<Mammal>> GetMammalsByHabitatId(int id, bool includeFamilies)
        {
            _logger.LogInformation($"Getting mammals in habitat by id: {id}");

            IQueryable<Mammal> query = _dBContext.Mammals
                .Where(i => i.MammalHabitats.Any(i => i.Habitat.HabitatID == id));

            if (query == null) throw new Exception($"Not found: { id }");

            if(includeFamilies)
            {
                query = query.Include(f => f.Family);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Mammal>> GetMammalsByLifeSpan(int fromYear, int toYear)
        {
            _logger.LogInformation($"Getting mammals by lifespan: {fromYear}-{toYear}");
            var query = _dBContext.Mammals
                .Where(x => x.Lifespan >= fromYear && x.Lifespan <= toYear);

            if (query == null) throw new Exception($"Not found: { fromYear } and { toYear }");


            return await query.ToListAsync(); 
        }

        public async Task<List<Mammal>> GetMammalsByFamily(string familyName, bool includeHabitat = false, bool includeFamily = false)
        {
            _logger.LogInformation($"Getting mammals by familyname {familyName}");

            var query = _dBContext.Mammals
                .Where(f => f.Family.Name == familyName);

            if (includeHabitat)
            {
                query = query.Include(m => m.MammalHabitats).ThenInclude(mh => mh.Habitat);
            }

            if (includeFamily)
            {
                query = query.Include(m => m.Family);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Mammal>> GetMammalsByFamilyId(int id, bool includeHabitat = false, bool includeFamily = false)
        {
            _logger.LogInformation($"Getting mammals by familyid {id}");

            var query = _dBContext.Mammals
                .Where(m => m.Family.FamilyId == id);

            if (includeHabitat)
            {
                query = query.Include(m => m.MammalHabitats).ThenInclude(mh => mh.Habitat);
            }

            if (includeFamily)
            {
                query = query.Include(m => m.Family);
            }

            return await query.ToListAsync();
        }

    }
}
