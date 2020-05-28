using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IMammalRepository : IRepository
    {
        Task<List<Mammal>> GetAllMammals();
        Task<Mammal> GetMammalById(int id);
        Task<List<Mammal>> GetMammalsByHabitatId(int id);
        Task<List<Mammal>> GetMammalsByHabitat(string habitatName);
        Task<List<Mammal>> GetMammalsByLifeSpan(int fromYear, int toYear, bool includeFamily, bool includeHabitat);
        Task<List<Mammal>> GetMammalsByFamily(string familyName);
        Task<List<Mammal>> GetMammalsByFamilyId(int id);
    }
}
