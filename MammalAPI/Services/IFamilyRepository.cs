using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IFamilyRepository:IRepository
    {
        Task<Family> GetFamilyByName(string name);
        Task<Family> GetFamilyById(int id, bool includeMammals = false);
        Task<Family[]> GetAllFamilies(bool includeMammals);
    }
}
