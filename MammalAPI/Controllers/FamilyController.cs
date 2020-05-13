using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyController(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetFamilyByName(string name)
        {
            return Ok(await _familyRepository.GetFamilyByName(name));
        }

        [HttpGet("byid/{id}")] 
        public async Task<IActionResult> GetFamilyById(int id)
        {
            return Ok(await _familyRepository.GetFamilyById(id));
        }
    }
}
