using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IHabitatRepository : IRepository
    {
        Task<List<IdNameDTO>> GetAllHabitats();
        Task<IdNameDTO> GetHabitatByName(string name);
        Task<IdNameDTO> GetHabitatById(int id);
        
    }
}
