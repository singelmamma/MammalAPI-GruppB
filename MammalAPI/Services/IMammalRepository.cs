using System.Collections.Generic;
using System.Threading.Tasks;
using MammalAPI.Models;

namespace MammalAPI.Services
{
    public interface IMammalRepository
    {
        Task<List<Mammal>> GetAllMammals();
    }
}