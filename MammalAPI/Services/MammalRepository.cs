using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;

namespace MammalAPI.Services
{
    public class MammalRepository : IMammalRepository
    {
        private readonly DBContextData dBContextData;

        public async Task<FakeMammal> GetFake()
        {
            var query = dBContextData.Get();
            return await Task.Run(() => query);

            //return await query.FirstOrDefaultAsync();
        }
    }
}