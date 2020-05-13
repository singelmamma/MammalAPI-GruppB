using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MammalAPI.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MammalAPI.Services
{
    public class MammalRepository : IMammalRepository
    {
        private readonly DBContext _dBContext;

        public MammalRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<List<Mammal>> GetAllMammals()
        {

            return await _dBContext.Mammals.ToListAsync();
        }

        public async Task<Mammal> GetMammalById(int id)
        {
            return await _dBContext.Mammals
                .FirstOrDefaultAsync(m => m.MammalId == id);
        }

        public async Task<List<IdNameDTO>> GetMammalsByHabitat(string habitatName)
        {
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

        public async Task<Mammal> GetMammalByLifeSpan(int lifespan)
        {
            return await _dBContext.Mammals
                .FirstOrDefaultAsync(m => m.Lifespan == lifespan);
        }

        public async Task<List<IdNameDTO>> GetMammalsByFamily(string familyName)
        {
            //var query = _dBContext.MammalHabitats
            //    .Include(mh => mh.Mammal)
            //    .AsNoTracking()
            //    .Where(x => x.HabitatId == id)
            //    .Select(x => new IdNameDTO
            //    {
            //        Id = x.MammalId,
            //        Name = x.Mammal.Name
            //    });

            //var query = _dBContext.Mammals
            //    .Include(m => m.Family)
            //    .AsNoTracking()
            //    .Where(m => m.Family.Name == name)
            //    .Select(x => new IdNameDTO
            //    {
            //        Id = x.MammalId,
            //        Name = x.Name
            //    });


            var families = _dBContext.Families.ToList();
            var familyID = families.Where(f => f.Name == familyName).FirstOrDefault();

            //var familyId = _dBContext.Families
            //    .Where(fam => fam.Name == name)
            //    .Select(fam => fam.FamilyId)
            //    .FirstOrDefault();

            var query = _dBContext.Mammals
                .Where(m => m.Family.FamilyId == 1)
                .Select(m => new IdNameDTO
                {
                    Id = m.MammalId,
                    Name = m.Name
                });

            return await query.ToListAsync();
        }

        public async Task<List<IdNameDTO>> GetMammalsByFamilyId(int id)
        {
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
