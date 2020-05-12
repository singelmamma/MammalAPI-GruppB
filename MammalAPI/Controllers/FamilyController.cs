using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("FamilyByName={name}")]
        public async Task<IActionResult> GetFamilyByName(string name)
        {
            var results = await _familyRepository.GetFamilyByName(name);
            return Ok(results);
        }

        [HttpGet("FamilyById={id}")] 
        public async Task<IActionResult> GetFamilyById(int id)
        {
            var results = await _familyRepository.GetFamilyById(id);
            return Ok(results);
        }
    }
}
