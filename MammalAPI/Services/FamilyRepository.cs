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

        public async Task<IdNameDTO> GetFamilyByName(string name)
        {
            _logger.LogInformation($"Getting mammal family by { name }.");
            var query = _dBContext.Families.Where(s => s.Name == name)
                .Select(s => new IdNameDTO
                {
                    Id = s.FamilyId,
                    Name = s.Name
                });

            if (query == null) throw new System.Exception($"Not found {name}");

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IdNameDTO> GetFamilyById(int id)
        {
            _logger.LogInformation($"Getting mammal family by { id }.");
            var query = _dBContext.Families.Where(s => s.FamilyId == id)
                .Select(s => new IdNameDTO
                {
                    Id = s.FamilyId,
                    Name = s.Name
                });

            if (query == null) throw new System.Exception($"Mammal family not found on id: {id}");

            return await query.FirstOrDefaultAsync();              
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
            if (query == null) throw new System.Exception($"Something went wrong.");


            return await query.ToListAsync();
        }
    }
}
