using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using AutoMapper;
using MammalAPI.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MammalAPI.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class FamilyController : HateoasControllerBase
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        public FamilyController(IFamilyRepository familyRepository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _familyRepository = familyRepository;
            this._mapper = mapper;
        }


        /// <summary>
        /// All Families
        /// </summary>
        /// <remarks>
        /// <h1>Get all Families and you can also include mammals!</h1>
        /// </remarks>
        [HttpGet(Name = "GetAllFamilies")]
        public async Task<IActionResult> GetAllFamilies([FromQuery]bool includeLinks = true, [FromQuery]bool includeMammals = false)
        {
            try
            {
                var results = await _familyRepository.GetAllFamilies(includeMammals);
                IEnumerable<FamilyDTO> mappedResult = _mapper.Map<FamilyDTO[]>(results);

                if (includeLinks)
                {
                    foreach (var family in mappedResult)
                    {
                        family.Mammals = family.Mammals.Select(m => HateoasMainLinks(m)).ToList();
                    }
                }

                if (includeLinks)
                {
                    IEnumerable<FamilyDTO> resultWithLinks = mappedResult.Select(r => HateoasMainLinks(r));
                    return Ok(resultWithLinks);
                }

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

        ///api/v1.0/family/1   Get family by id
        
        /// <summary>
        /// Get Family by ID
        /// </summary>
        /// <remarks>
        /// <h1>Get specific family and you can also include mammals!</h1>
        /// </remarks>
        [HttpGet("{id:int}", Name = "GetFamilyByIdAsync")]
        public async Task<IActionResult> GetFamilyById(int id, [FromQuery]bool includeLinks = true, [FromQuery]bool includeMammals = false)
        {
            try
            {
                var result = await _familyRepository.GetFamilyById(id, includeMammals);
                var mappedResult = _mapper.Map<FamilyDTO>(result);
                
                if (includeLinks)
                {
                    mappedResult.Mammals = mappedResult.Mammals.Select(m => HateoasMainLinks(m)).ToList();  
                }

                if (includeLinks)
                {
                    return Ok(HateoasMainLinks(mappedResult));
                }

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

        ///api/v1.0/family/Phocidae      Get family by name

        /// <summary>
        /// Get Family by Name
        /// </summary>
        /// <remarks>
        /// <h1>Get specific family and you can also include mammals!</h1>
        /// </remarks>
        [HttpGet("{name}")]
        public async Task<ActionResult> GetFamilyByName(string name, [FromQuery] bool includeLinks = true, [FromQuery] bool includeMammals = false)
        {
            try
            {
                var result = await _familyRepository.GetFamilyByName(name, includeMammals);
                var mappedResult = _mapper.Map<FamilyDTO>(result);

                if (includeLinks)
                {
                    mappedResult.Mammals = mappedResult.Mammals.Select(m => HateoasMainLinks(m)).ToList();
                }

                if (includeLinks)
                {
                    return Ok(HateoasMainLinks(mappedResult));
                }

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

        /// <summary>
        /// Post Family
        /// </summary>
        /// <remarks>
        /// <h1>Post Info in family</h1>
        /// </remarks>
        [HttpPost(Name = "PostFamily")]
        [ApiKeyAuthentication]
        public async Task<ActionResult<FamilyDTO>> PostFamily(FamilyDTO familyDTO)
        {
            try
            {
                var mappedEntity = _mapper.Map<Family>(familyDTO);
                _familyRepository.Add(mappedEntity);
                if (await _familyRepository.Save())
                {
                    return Created($"/api/v1.0/family/id{familyDTO.FamilyID}", _mapper.Map<FamilyDTO>(mappedEntity));
                }
            }

            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"database failure {e.Message}");
            }

            return BadRequest();
        }

        ///api/v1.0/family/##       Put a family by id
        
        /// <summary>
        /// Put Family by ID
        /// </summary>
        /// <remarks>
        /// <h1>Put family bu specific Id</h1>
        /// </remarks>
        [HttpPut("{familyId}", Name = "PutFamily")]
        [ApiKeyAuthentication]
        public async Task<ActionResult<FamilyDTO>> PutFamily (int familyId, FamilyDTO familyDTO)
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

        /// <summary>
        /// Delete family by id
        /// </summary>
        /// <remarks>
        /// <h1>Delete family by Id!</h1>
        /// </remarks>
        [HttpDelete("{familyId}", Name = "DeleteFamily")]
        [ApiKeyAuthentication]
        public async Task<ActionResult<FamilyDTO>> DeleteFamily (int familyId)
        {
            try
            {
                var familyToDelete = await _familyRepository.GetFamilyById(familyId);
                if (familyToDelete == null)
                {
                    return NotFound($"Family with ID: {familyId} could not be found.");
                }

                _familyRepository.Delete(familyToDelete);

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
    }
}
