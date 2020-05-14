using MammalAPI.Services;
using Microsoft.AspNetCore.Http;
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
            try
            {
                return Ok(await _habitatRepository.GetHabitatByName(habitatName));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabitatById(int id)
        {
            try
            {
                return Ok(await _habitatRepository.GetHabitatById(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllHabitats()
        {
            try
            {
                return Ok(await _habitatRepository.GetAllHabitats());
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

    }
}