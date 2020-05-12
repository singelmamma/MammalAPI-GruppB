using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class MammalController : ControllerBase
    {
        private readonly IMammalRepository _repository;

        public MammalController(IMammalRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAllMammals());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMammalById(int id)
        {
            return Ok(await _repository.GetMammalById(id));
        }

        [HttpGet("habitat/{habitatName}")]
        public async Task<IActionResult> GetMammalsByHabitat(string habitatName)
        {
            return Ok(await _repository.GetMammalsByHabitat(habitatName));
        }

        [HttpGet()]
        public async Task<IActionResult> GetMammalsByHabitatId([FromQuery] int habitatId)
        {
            return Ok(await _repository.GetMammalsByHabitatId(habitatId));
        }
    }
}
