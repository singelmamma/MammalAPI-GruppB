using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IMammalRepository : IRepository
    {
        Task<List<Mammal>> GetAllMammals();
        Task<List<Mammal>> GetMammalByName(string mammelName, bool includeFamilies);
        Task<Mammal> GetMammalById(int id);
        Task<List<Mammal>> GetMammalsByHabitatId(int id);
        Task<List<Mammal>> GetMammalsByHabitat(string habitatName);
        Task<List<Mammal>> GetMammalsByLifeSpan(int fromYear, int toYear);
        Task<List<Mammal>> GetMammalsByFamily(string familyName);
        Task<List<Mammal>> GetMammalsByFamilyId(int id);
    }
}
