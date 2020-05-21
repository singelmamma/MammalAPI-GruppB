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
        Task<List<IdNameDTO>> GetMammalsByHabitat(string habitatName);
        Task<List<IdNameDTO>> GetMammalsByHabitatId(int id);
        Task<List<MammalLifespanDTO>> GetMammalsByLifeSpan(int fromYear, int toYear);
        Task<List<IdNameDTO>> GetMammalsByFamily(string familyName);
        Task<List<IdNameDTO>> GetMammalsByFamilyId(int id);
    }
}
