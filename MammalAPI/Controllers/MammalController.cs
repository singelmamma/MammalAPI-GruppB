using AutoMapper;
using MammalAPI.DTO;
using MammalAPI.Models;
using MammalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class MammalController : HateoasMammalControllerBase
    {
        private readonly IMammalRepository _repository;
        private readonly IMapper _mapper;

        public MammalController(IMammalRepository repository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetAll", Name ="GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllMammals();
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                IEnumerable<MammalDTO> mammalsresult = mappedResult.Select(m => HateoasMainLinks(m));

                return Ok(mammalsresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: { e.Message }");
            }
        }

        [HttpGet("{id:int}", Name = "GetMammalAsync")]
        public async Task<IActionResult> GetMammalById(int id)
        {
            try
            {
                var result = await _repository.GetMammalById(id);
                var mappedResult = _mapper.Map<MammalDTO>(result);

                return Ok(HateoasMainLinks(mappedResult));
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
                var result = await _repository.GetMammalsByHabitat(habitatName);
                var mappedResult = _mapper.Map<List<MammalDTO>>(result);
                return Ok(mappedResult);
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

        [HttpGet("lifespan/fromYear={fromYear}&toYear={toYear}")]
        public async Task<IActionResult> GetMammalByLifeSpan(int fromYear, int toYear)
        {
            try
            {
                var result= await _repository.GetMammalsByLifeSpan(fromYear, toYear);
                var mappedResult = _mapper.Map<MammalLifespanDTO>(result);
                return Ok(mappedResult);
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
                var result= await _repository.GetMammalsByFamily(familyName);
                var mappedResult = _mapper.Map<MammalDTO>(result);
                return Ok(mappedResult);
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
                var result= await _repository.GetMammalsByFamilyId(id);
                var mappedResult = _mapper.Map<List<MammalDTO>>(result);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }
        [HttpPost]
        public async Task<ActionResult<MammalDTO>> PostMammal(MammalDTO mammalDTO)
        {
            try
            {
                var mappedEntity = _mapper.Map<Mammal>(mammalDTO);

                _repository.Add(mappedEntity);
                if(await _repository.Save())
                {
                    return Created($"api/v1.0/mammals/{mappedEntity.MammalId}", _mapper.Map<MammalDTO>(mappedEntity));
                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure : {e.Message}");
            }
            return BadRequest();
        }
    }
}
