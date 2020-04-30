using System.Threading.Tasks;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public class MammalRepository : IMammalRepository
    {
        //private readonly DBContext dBContext;

        public async Task<Fake> GetFake()
        {
            var query = dbcontext.Fake;
            return await query.FirstOrDefaultAsync();
        }
    }
}