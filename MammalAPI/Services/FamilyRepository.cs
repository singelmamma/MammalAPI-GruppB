using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Context;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            
        }

        public async Task<Family> GetFamilyBy(int id)
        {

        }
    }
}
