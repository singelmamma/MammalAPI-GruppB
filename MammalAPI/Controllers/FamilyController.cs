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

        [HttpGet("MammalsByFamilyName={name}")]
        public async Task<IActionResult> ByFamiliyName()
        {
            var results = await _familyRepository.GetMammalsByFamilyName(string name);
            return Ok(results);
        }

        [HttpGet("MammalsByFamilyId={id}")] 
        public async Task<IActionResult> ByFamiliyId()
        {
            var results = await _familyRepository.GetMammalsByFamilyId(int id);
            return Ok(results);
        }
    }
}
