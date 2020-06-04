using AutoMapper;
using MammalAPI.Authentication;
using MammalAPI.DTO;
using MammalAPI.Models;
using MammalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;



namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class HabitatController : HateoasControllerBase
    {

        private readonly IHabitatRepository _habitatRepository;
        private readonly IMapper _mapper;

        public HabitatController(IHabitatRepository habitatRepository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _habitatRepository = habitatRepository;
            _mapper = mapper;
        }

        ///api/v1.0/habitat             To get all habitats
        
        /// <summary>
        /// Get all Habitat 
        /// </summary>
        /// <remarks>
        /// <h1>Get all Habitat!</h1>
        /// </remarks>
        [HttpGet(Name = "GetAllHabitat")]
        public async Task<ActionResult<HabitatDTO[]>> GetAllHabitats(bool includeMammal = false)
        {
            try
            {
                if (_habitatRepository == null)
                {
                    return NotFound();
                }
                var result = await _habitatRepository.GetAllHabitats(includeMammal);
                IEnumerable<HabitatDTO> mappedResult = _mapper.Map<HabitatDTO[]>(result);

                if (includeMammal)
                {
                    foreach (var habitat in mappedResult)
                    {
                        habitat.Mammal = habitat.Mammal.Select(m => HateoasMainLinks(m)).ToList();
                    }
                }

                IEnumerable<HabitatDTO> habitatResult = mappedResult.Select(h => HateoasMainLinks(h));
                return Ok(habitatResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        ///api/v1.0/habitat/1    To get one habitat by id
        
        /// <summary>
        /// Get specific Habitat by Id
        /// </summary>
        /// <remarks>
        /// <h1>Get specific Habitat by Id and you can include mammals!</h1>
        /// </remarks>
        [HttpGet("{id:int}", Name = "GetHabitatByID")]
        public async Task<ActionResult<HabitatDTO>> GetHabitatById(int id, [FromQuery]bool includeMammal=false)
        {
            try
            {
                var result = await _habitatRepository.GetHabitatById(id, includeMammal);
                var mappedResult = _mapper.Map<HabitatDTO>(result);

                if (includeMammal)
                {
                    mappedResult.Mammal = mappedResult.Mammal.Select(m => HateoasMainLinks(m)).ToList();
                }
                return Ok(HateoasMainLinks(mappedResult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        ///api/v1.0/habitat/=pacific ocean                To get habitat by name

        /// <summary>
        /// Get Habitat by specific Name
        /// </summary>
        /// <remarks>
        /// <h1>Get Habitat by Name and you can include mammals!</h1>
        /// </remarks>
        [HttpGet("{name}", Name ="GetHabitatByName")]
        public async Task<IActionResult> GetHabitatByName(string name, [FromQuery] bool includeLinks = true, [FromQuery] bool includeMammal = false)
        {
            try
            {
                var result= await _habitatRepository.GetHabitatByName(name, includeMammal);
                var mappedResult = _mapper.Map<HabitatDTO>(result);

                if (includeLinks)
                {
                    mappedResult = HateoasMainLinks(mappedResult);
                    mappedResult.Mammal = mappedResult.Mammal.Select(m => HateoasMainLinks(m)).ToList();
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
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: {e.Message}");
            }
        }

        ///api/v1.0/habitat           To create a post
        
        /// <summary>
        /// Post Habitat by name
        /// </summary>
        /// <remarks>
        /// <h1>Post Habitat by name!</h1>
        /// </remarks>
        [ApiKeyAuthentication]
        [HttpPost(Name = "postHabitat")]
        public async Task<ActionResult<HabitatDTO>> PostHabitat(HabitatDTO habitatDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Habitat>(habitatDto);
                _habitatRepository.Add(mappedEntity);
                if (await _habitatRepository.Save())
                {
                    return Created($"/api/v1.0/habitat/id/{habitatDto.HabitatID}", _mapper.Map<HabitatDTO>(mappedEntity));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"database failure {e.Message}");
            }
            return BadRequest();
        }

        ///api/v1.0/habitat/8       To change a habitat

        /// <summary>
        /// Put Habitat by Id
        /// </summary>
        /// <remarks>
        /// <h1>Put Habitat by id!</h1>
        /// </remarks>
        [ApiKeyAuthentication]
        [HttpPut("{id}", Name = "putHabitat")]
        public async Task<ActionResult> PutHabitat(int id, HabitatDTO habitatDto)
        {
            try
            {
                var oldHabitat = await _habitatRepository.GetHabitatById(id);
                if (oldHabitat == null)
                {
                    return NotFound();
                }

                var newHabitat = _mapper.Map(habitatDto, oldHabitat);
                _habitatRepository.Update(newHabitat);
                if (await _habitatRepository.Save())
                {
                    return NoContent();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete specific Habitat by Id
        /// </summary>
        /// <remarks>
        /// <h1>Delete specific Habitat by Id!</h1>
        /// </remarks>
        [ApiKeyAuthentication]
        [HttpDelete("{habitatId}", Name = "deleteHabitat")]
        public async Task<ActionResult<HabitatDTO>> DeleteHabitat (int habitatId)
        {
            try
            {
                var habitatToDelete = await _habitatRepository.GetHabitatById(habitatId);
                if(habitatToDelete == null)
                {
                    return NotFound($"Habitat with ID: {habitatId} could not be found");
                }

                _habitatRepository.Delete(habitatToDelete);

                if(await _habitatRepository.Save())
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
