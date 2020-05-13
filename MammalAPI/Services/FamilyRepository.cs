using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
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
            _logger.LogInformation($"Getting mammal family by {name}.");
            return await _dBContext.Families.Where(s => s.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Family> GetFamilyById(int id)
        {
            _logger.LogInformation($"Getting mammal family by {id}.");
            return await _dBContext.Families.Where(s => s.FamilyId == id).FirstOrDefaultAsync();
        }
    }
}
