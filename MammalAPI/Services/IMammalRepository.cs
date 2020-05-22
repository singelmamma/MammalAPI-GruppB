using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IMammalRepository : IRepository
    {
        Task<List<Mammal>> GetAllMammals();
        Task<MammalDTO> GetMammalById(int id);
        Task<List<FamilyDTO>> GetMammalsByHabitat(string habitatName);
        Task<List<FamilyDTO>> GetMammalsByHabitatId(int id);
        Task<List<MammalLifespanDTO>> GetMammalsByLifeSpan(int fromYear, int toYear);
        Task<List<FamilyDTO>> GetMammalsByFamily(string familyName);
        Task<List<FamilyDTO>> GetMammalsByFamilyId(int id);
    }
}
