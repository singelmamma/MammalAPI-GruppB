using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                return Ok(await _repository.GetAllMammals());
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: { e.Message }");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMammalById(int id)
        {
            try
            {
                var result = await _repository.GetMammalById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("habitat/{habitatName}")]
        public async Task<IActionResult> GetMammalsByHabitat(string habitatName)
        {
            try
            {
                return Ok(await _repository.GetMammalsByHabitat(habitatName));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMammalsByHabitatId([FromQuery] int habitatId)
        {
            try
            {
                return Ok(await _repository.GetMammalsByHabitatId(habitatId));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("lifespan/{fromLifespanYear}&{toLifepanYear}")]
        public async Task<IActionResult> GetMammalByLifeSpan(int fromLifespanYear,int toLifespanYear)
        {
            try
            {
                return Ok(await _repository.GetMammalsByLifeSpan(fromLifespanYear, toLifespanYear));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("byfamilyname/{familyName}")]
        public async Task<IActionResult> GetMammalsByFamilyName(string familyName)
        {
            try
            {
                return Ok(await _repository.GetMammalsByFamily(familyName));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("byfamilyid/{id}")]
        public async Task<IActionResult> GetMammalsByFamilyId(int id)
        {
            try
            {
                return Ok(await _repository.GetMammalsByFamilyId(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }
    }
}
