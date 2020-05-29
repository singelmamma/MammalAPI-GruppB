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
using Microsoft.AspNetCore.Mvc;

namespace MammalAPI.Services
{
    public class MammalRepository : Repository, IMammalRepository
    {
        public MammalRepository(DBContext DBContext, ILogger<MammalRepository> logger): base(DBContext, logger)
        {}

        public async Task<List<Mammal>> GetAllMammals(bool includeFamily = false, bool includeHabitat = false)
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

        public async Task<List<Mammal>> GetMammalByName(string mammalName, bool includeFamilies)
        {
            _logger.LogInformation($"Getting mammals by name: {mammalName}");

            IQueryable<Mammal> query = _dBContext.Mammals
                .Where(x => x.MammalHabitats.Any(z => z.Mammal.Name == mammalName));

            if (query == null) throw new Exception($"Not found: { mammalName }");

            if(includeFamilies)
            {
                query = query.Include(f => f.Family);
            }

            return await query.ToListAsync();
        }

        public async Task<Mammal> GetMammalById(int id, bool includeFamily = false, bool includeHabitat = false)
        {
            _logger.LogInformation($"Getting mammal with {id}");

            IQueryable<Mammal> query = _dBContext.Mammals.Where(m => m.MammalId == id);

            if (query == null) throw new Exception($"Not found: { id }");
            
            if (includeFamily)
            {
                query = query.Include(f => f.Family);
            }
            if (includeHabitat)
            {
                query = query.Include(mh => mh.MammalHabitats).ThenInclude(h => h.Habitat);
            }

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

        public async Task<List<Mammal>> GetMammalsByHabitatId(int id, bool includeFamily = false, bool includeHabitat = false)
        {
            _logger.LogInformation($"Getting mammals in habitat by id: {id}");

            IQueryable<Mammal> query = _dBContext.Mammals
                .Where(i => i.MammalHabitats.Any(i => i.Habitat.HabitatID == id));

            if (query == null) throw new Exception($"Not found: { id }");

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

        public async Task<List<Mammal>> GetMammalsByLifeSpan(int fromYear, int toYear, bool includeFamily, bool includeHabitat)
        {
            _logger.LogInformation($"Getting mammals by lifespan: {fromYear}-{toYear}");
            
            var query = _dBContext.Mammals
                .Where(x => x.Lifespan >= fromYear && x.Lifespan <= toYear);

            if (query == null) throw new Exception($"Not found: { fromYear } and { toYear }");

            if(includeFamily)
            {
                query = query.Include(f => f.Family);
            }
            if(includeHabitat)
            {
                query = query.Include(mh => mh.MammalHabitats)
                    .ThenInclude(h => h.Habitat);
            }

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
