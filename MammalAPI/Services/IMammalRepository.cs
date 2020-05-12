using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IMammalRepository
    {
        Task<List<Mammal>> GetAllMammals();
        Task<Mammal> GetMammalById(int id);
        Task<List<IdNameDTO>> GetMammalsByHabitat(string habitatName);
<<<<<<< HEAD
        Task<List<IdNameDTO>> GetMammalsByHabitatId(int id);
=======

        Task<Mammal> GetMammalByLifeSpan(int lifespan);
>>>>>>> d2df0eddecdd3136500576aeed9f1dcb0dc080d5
    }
}
