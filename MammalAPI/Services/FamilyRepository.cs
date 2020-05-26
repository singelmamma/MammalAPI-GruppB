using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MammalAPI.DTO;
using Microsoft.Extensions.Logging;





namespace MammalAPI.Services
{
    public class FamilyRepository : Repository, IFamilyRepository
    {
        public FamilyRepository(DBContext DBContext, ILogger<FamilyRepository> logger) : base (DBContext, logger)
        { }

        public async Task<Family> GetFamilyByName(string name)
        {
            _logger.LogInformation($"Getting mammal family by { name }.");
            var query = _dBContext.Families.Where(f => f.Name == name);

            if (query == null) throw new System.Exception($"Not found {name}");

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Family> GetFamilyById(int id)
        {
            _logger.LogInformation($"Getting mammal family by { id }.");
            var query = _dBContext.Families.Where(f => f.FamilyId == id);

            if (query == null) throw new System.Exception($"Mammal family not found on id: {id}");

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task<Family[]> GetAllFamilies(bool includeMammals)
        {
            IQueryable<Family> queary = _dBContext.Families;

            if (includeMammals == true)
            {
                queary = queary.Include(m => m.Mammals);
            }

            return await queary.ToArrayAsync();
        }
    }
}
