using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AutoMapper;
using MammalAPI.Models;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        public FamilyController(IFamilyRepository familyRepository, IMapper mapper)
        {
            _familyRepository = familyRepository;
            this._mapper = mapper;
        }

        ///api/v1.0/family/byname/Phocidae      Get family by name
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetFamilyByName(string name)
        {
            try
            {
                var result= await _familyRepository.GetFamilyByName(name);
                var mappedResult = _mapper.Map<FamilyDTO>(result);
                return Ok(mappedResult);
            }
            catch (TimeoutException e)
            {
                return this.StatusCode(StatusCodes.Status408RequestTimeout, $"Request timeout: {e.Message}");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        ///api/v1.0/family/byid/1   Get family by id
        [HttpGet("byid/{id}")] 
        public async Task<IActionResult> GetFamilyById(int id)
        {
            try
            {
                var result = await _familyRepository.GetFamilyById(id);
                var mappedResult = _mapper.Map<FamilyDTO>(result);
                return Ok(mappedResult);

            }
            catch (TimeoutException e)
            {
                return this.StatusCode(StatusCodes.Status408RequestTimeout, $"Request timeout: {e.Message}");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");

            }
        }

        ///api/v1.0/family/all       Get all families
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFamilies()
        {
            try
            {
                return Ok(await _familyRepository.GetAllFamilies());
            }
            catch (TimeoutException e)
            {
                return this.StatusCode(StatusCodes.Status408RequestTimeout, $"Request timeout: {e.Message}");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IdNameDTO>>PostFamily(IdNameDTO family)
        {
            try
            {
                var mappedEntity = _mapper.Map<Family>(family);
                _familyRepository.Add(mappedEntity);

                if (await _familyRepository.Save())
                {
                    return Created($"/api/v1.0/Family/{mappedEntity.FamilyId}", _mapper.Map<IdNameDTO>(mappedEntity));
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
