using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AutoMapper;
using MammalAPI.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class FamilyController : HateoasFamilyControllerBase
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        public FamilyController(IFamilyRepository repository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _familyRepository = repository;
            _mapper = mapper;
        }

        ///api/v1.0/family/byname/Phocidae      Get family by name
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<FamilyDTO>> GetFamilyByName(string name)
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
        [HttpGet("{id:int}", Name = "GetFamilyAsync")] 
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
        [HttpGet("GetAll", Name = "GetAllFamily")]
        public async Task<IActionResult> Get([FromQuery]bool includeMammals)
        {
            try
            {
                var results = await _familyRepository.GetAllFamily(includeMammals);
                IEnumerable<FamilyDTO> mappedResult = _mapper.Map<FamilyDTO[]>(results);
                IEnumerable<FamilyDTO> familysresult = mappedResult.Select(m => HateoasMainLinks(m));
                
                return Ok(familysresult);
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

        ///api/v1.0/family/##       Put a family by id
        [HttpPut("{familyId}")]
        public async Task<ActionResult<FamilyDTO>> Put(int familyId, FamilyDTO familyDTO)
        {
            try
            {
                var oldFamily = await _familyRepository.GetFamilyById(familyId);
                if (oldFamily == null)
                {
                    return NotFound($"Family with ID: {familyId} could not be found.");
                }

                var newFamily = _mapper.Map(familyDTO, oldFamily);
                _familyRepository.Update(newFamily);
                if (await _familyRepository.Save())
                {
                    return NoContent();
                }
            }
            catch (TimeoutException e)
            {
                return this.StatusCode(StatusCodes.Status408RequestTimeout, $"Request timeout: {e.Message}");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }

            return BadRequest();
        }


        /*
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

        }        */


    }
}
