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
    public class HabitatController : ControllerBase
    {

        private readonly IHabitatRepository _habitatRepository;

        public HabitatController(IHabitatRepository habitatRepository)
        {
            _habitatRepository = habitatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHabitatByName([FromQuery]string habitatName)
        {
            return Ok(await _habitatRepository.GetHabitatByName(habitatName));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabitatById(int id)
        {
            return Ok(await _habitatRepository.GetHabitatById(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllHabitats()
        {
            return Ok(await _habitatRepository.GetAllHabitats());
        }

    }
}