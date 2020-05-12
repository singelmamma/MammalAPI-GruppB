using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IHabitatRepository
    {
        Task<List<Habitat>> GetAllHabitats();

        Task<Habitat> GetHabitatById(int id);
    }
}
