using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
           
            var query = _dBContext.Mammals.Where(f => f.MammalId == 1).ToList();
            return await Task.Run(() => query);
        }
    }
}
