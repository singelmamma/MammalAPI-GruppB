using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IFamilyRepository
    {
        Task<IdNameDTO> GetFamilyByName(string name);
        Task<IdNameDTO> GetFamilyById(int id);
        Task<List<IdNameDTO>> GetAllFamilies();
    }
}
