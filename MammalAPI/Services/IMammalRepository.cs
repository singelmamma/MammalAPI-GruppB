using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IMammalRepository : IRepository
    {
        Task<List<Mammal>> GetAllMammals(bool includeFamily = false, bool includeHabitat = false);
        Task<Mammal> GetMammalById(int id, bool includeFamily = false, bool includeHabitat = false);
        Task<List<Mammal>> GetMammalByName(string mammalName, bool includeFamilies = false);  
        Task<List<Mammal>> GetMammalsByLifeSpan(int fromYear, int toYear, bool includeFamily = false, bool includeHabitat = false);
        Task<List<Mammal>> GetMammalsByHabitatId(int id, bool includeFamily = false, bool includeHabitat = false);
        Task<List<Mammal>> GetMammalsByHabitat(string habitatName);
        Task<List<Mammal>> GetMammalsByLifeSpan(int fromYear, int toYear, bool includeFamily = false, bool includeHabitat = false);
        Task<List<Mammal>> GetMammalsByFamily(string familyName, bool includeHabitat = false, bool includeFamily = false);
        Task<List<Mammal>> GetMammalsByFamilyId(int id, bool includeHabitat = false, bool includeFamily = false);
        Task<List<Mammal>> GetMammalsByFamily(string familyName, bool includeHabitat = false, bool includeFamily = false);
    }
}
