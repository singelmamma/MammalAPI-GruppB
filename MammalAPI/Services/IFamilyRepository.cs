using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IFamilyRepository
    {
        Task<Family> GetFamilyByName(string name);
        Task<Family> GetFamilyById(int id);
        Task<List<IdNameDTO>> GetAllFamilies();
    }
}
