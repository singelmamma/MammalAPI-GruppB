using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IHabitatRepository : IRepository
    {
        Task<List<FamilyDTO>> GetAllHabitats();
        Task<FamilyDTO> GetHabitatByName(string name);
        Task<HabitatDTO> GetHabitatById(int id);
        
    }
}
