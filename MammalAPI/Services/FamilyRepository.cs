using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MammalAPI.DTO;


namespace MammalAPI.Services
{
    class FamilyRepository : IFamilyRepository
    {
        private readonly DBContext _dBContext;

        public FamilyRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Family> GetFamilyByName(string name)
        {
            return await _dBContext.Families.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<Family> GetFamilyById(int id)
        {
            return await _dBContext.Families.FirstOrDefaultAsync(s => s.FamilyId == id);
        }
    }
}
