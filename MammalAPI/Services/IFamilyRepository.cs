using System.Threading.Tasks;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IFamilyRepository : IRepository
    {
<<<<<<< HEAD
        Task<Family[]> GetAllFamilies(bool includeMammals);
        Task<Family> GetFamilyById(int id, bool includeMammals = false);
        Task<Family> GetFamilyByName(string name, bool includeMammals = false);
=======
        Task<Family> GetFamilyByName(string name);
        Task<Family> GetFamilyById(int id);
        Task<Family[]> GetAllFamily(bool includeMammals);
>>>>>>> @MirkoPralica Tested HateOas
    }
}
