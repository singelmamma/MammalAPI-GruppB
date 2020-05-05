using System.Threading.Tasks;
using MammalAPI.Models;
using MammalAPI.Context;

namespace MammalAPI.Services
{
    public class MammalRepository : IMammalRepository
    {
        // private readonly DBContextData _dBContextData;

        // public MammalRepository(DBContextData dBContextData)
        // {
        //     _dBContextData = dBContextData;
        // }

        // public async Task<FakeMammal> GetFake()
        // {
        //     var query = _dBContextData.Get();
        //     return await Task.Run(() => query);

        //     //return await query.FirstOrDefaultAsync();
        // }
    }
}
