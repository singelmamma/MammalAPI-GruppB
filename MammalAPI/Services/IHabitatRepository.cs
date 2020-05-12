using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IHabitatRepository
    {
        Task<List<Habitat>> GetAllHabitats();
        Task<IdNameDTO> GetHabitatByName(string name);
    }
}
