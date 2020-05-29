using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IHabitatRepository : IRepository
    {
        Task<List<Habitat>> GetAllHabitats(bool includeMammal=false);
        Task<Habitat> GetHabitatById(int id, bool includeMammal = false);
        Task<Habitat> GetHabitatByName(string name, bool includeMammal=false);        
    }
}
