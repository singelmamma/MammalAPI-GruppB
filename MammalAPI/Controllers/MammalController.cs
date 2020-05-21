using AutoMapper;
using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MammalAPI.Models;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class MammalController : HateoasControllerBase
    {
        private readonly IMammalRepository _repository;
        private readonly IMapper _mapper;

        public MammalController(IMammalRepository repository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("getall", Name = "GetClients")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllMammals();
                
                var mappedResult = _mapper.Map<MammalDTO[]>(results);

                //IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                //IEnumerable<MammalDTO> mammalDto = mappedResult.Select(m => RestfulClient(m));

                //return Ok(mappedResult);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: { e.Message }");
            }
        }

        [HttpGet("{id:int}", Name = "GetClientAsync")]
        public async Task<IActionResult> GetMammalById(int id)
        {
            try
            {
                var result = await _repository.GetMammalById(id);
                return Ok(RestfulClient(result));
                //return Ok(result);
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

        [HttpGet("habitat/{habitatId}")]
        public async Task<IActionResult> GetMammalsByHabitatId(int habitatId)
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
                return Ok(await _repository.GetMammalsByLifeSpan(fromYear, toYear));
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
