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
    class FamilyRepository : Repository, IFamilyRepository
    {
        public FamilyRepository(DBContext DBContext, ILogger<FamilyRepository> logger) : base (DBContext, logger)
        { }

        public async Task<Family> GetFamilyByName(string name)
        {
            _logger.LogInformation($"Getting mammal family by { name }.");
            return await _dBContext.Families.Where(s => s.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Family> GetFamilyById(int id)
        {
            _logger.LogInformation($"Getting mammal family by { id }.");
            return await _dBContext.Families.Where(s => s.FamilyId == id).FirstOrDefaultAsync();
        }

        public async Task<List<IdNameDTO>> GetAllFamilies()
        {
            _logger.LogInformation($"Getting all families");
            var query = _dBContext.Families
                .Select(x => new IdNameDTO
                {
                    Id = x.FamilyId,
                    Name = x.Name
                });

            return await query.ToListAsync();

        }
    }
}
