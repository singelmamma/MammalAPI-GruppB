using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IFamilyRepository:IRepository
    {
        Task<Family> GetFamilyByName(string name);
        Task<FamilyDTO> GetFamilyById(int id);
        Task<Family[]> GetAllFamilies(bool includeMammals);
    }
}
