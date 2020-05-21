using AutoMapper;
using MammalAPI.DTO;
using MammalAPI.Models;
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
        private readonly IMapper _mapper;

        public HabitatController(IHabitatRepository habitatRepository, IMapper mapper)
        {
            _mapper = mapper;
            _habitatRepository = habitatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHabitatByName([FromQuery] string habitatName)
        {
            try
            {
                return Ok(await _habitatRepository.GetHabitatByName(habitatName));
            }
            catch (TimeoutException e)
            {
                return this.StatusCode(StatusCodes.Status408RequestTimeout, $"Request timeout: {e.Message}");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabitatById(int id)
        {
            try
            {
                var result =await _habitatRepository.GetHabitatById(id);
                var mappedResult = _mapper.Map<HabitatDTO>(result);
                return Ok(mappedResult);
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
                var result= await _habitatRepository.GetAllHabitats();
                var mappedResult = _mapper.Map<HabitatDTO>(result);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IdNameDTO>> PostHabitat(IdNameDTO habitat)
        {
            try
            {
                var mappedEntity = _mapper.Map<Habitat>(habitat);
                _habitatRepository.Add(mappedEntity);

                if (await _habitatRepository.Save())
                {
                    return Created($"/api/v1.0/Habitat/{mappedEntity.HabitatID}", _mapper.Map<IdNameDTO>(mappedEntity));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"database failure {e.Message}");
            }

            return BadRequest();
        }
    }
}
