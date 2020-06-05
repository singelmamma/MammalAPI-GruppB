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
using MammalAPI.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class MammalsController : HateoasControllerBase
    {
        private readonly IMammalRepository _repository;
        private readonly IMapper _mapper;

        public MammalsController(IMammalRepository repository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Mammals
        /// </summary>
        /// <remarks>
        /// <h1>Get all Mammals and you can also include Family and Habitat!</h1>
        /// </remarks>
        [HttpGet(Name ="GetAll")]
        public async Task<ActionResult<MammalDTO[]>> Get([FromQuery]bool includeLinks = true, [FromQuery]bool includeFamily = false, [FromQuery]bool includeHabitat = false)
        {
            try
            {
                var results = await _repository.GetAllMammals(includeFamily, includeHabitat);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();
                if (results == null) throw new System.Exception($"Cant find all Mammals");
                if (includeLinks)
                {

                    if (includeHabitat)
                    {
                        foreach (var mammal in mappedResult)
                        {
                            mammal.Habitats = mammal.Habitats.Select(m => HateoasMainLinks(m)).ToList();
                        }
                    }
                    if (includeFamily)
                    {
                        foreach (MammalDTO mammal in mappedResult)
                        {
                            if (mammal.Family != null)
                            {
                                if (!items.ContainsKey(mammal.Family.Name))
                                {
                                    items.Add(mammal.Family.Name, HateoasMainLinks(mammal.Family));
                                }
                                mammal.Family.Mammals = null;
                            }
                        }

                        foreach (MammalDTO mammal in mappedResult)
                        {
                            if (mammal.Family != null)
                            {
                                mammal.Family.Mammals = items[mammal.Family.Name].Mammals;
                            }
                        }
                    }
                    mappedResult = mappedResult.Select(m => HateoasMainLinks(m));
                    return Ok(mappedResult);
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: { e.Message }");
            }
        }

        /// <summary>
        /// Get specific Mammal by Id
        /// </summary>
        /// <remarks>
        /// <h1>Get specific Mammal by id and you can also include Family and Habitat!</h1>
        /// </remarks>
        [HttpGet("{id:int}", Name="GetMammalAsync")]
        public async Task<IActionResult> GetMammalById(int id, [FromQuery] bool includeLinks = true, [FromQuery] bool includeFamily = false, bool includeHabitat = false)
        {
            try
            {                
            
                var result = await _repository.GetMammalById(id, includeFamily, includeHabitat);
                var mappedResult = _mapper.Map<MammalDTO>(result);
                if (result == null) throw new System.Exception($"Mammal with {id} does not exist");
                if (includeLinks)
                {
                    if (includeFamily == true) 
                    {
                        mappedResult.Family.Mammals = null;
                        mappedResult.Family = HateoasMainLinks(mappedResult.Family);
                    } 

                    if(includeHabitat == true) mappedResult.Habitats = mappedResult.Habitats.Select(m => HateoasMainLinks(m)).ToList();

                    return Ok(HateoasMainLinks(mappedResult));
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        /// <summary>
        /// Get Mammal by name
        /// </summary>
        /// <remarks>
        /// <h1>Get Mammal by Name and you can also include Family!</h1>
        /// </remarks>
        [HttpGet("{mammalName}", Name="GetMammalName")]
        public async Task<IActionResult> GetMammalByName(string mammalName, [FromQuery] bool includeLinks = true, [FromQuery] bool includeFamilies = false)
        {
            try
            {
                var result = await _repository.GetMammalByName(mammalName, includeFamilies);
                var mappedResult = _mapper.Map<MammalDTO>(result);
                if (result == null) throw new System.Exception($"Mammal with {mammalName} does not exist");
                if (includeFamilies)
                {
                    mappedResult.Family.Mammals = null;
                }

                if (includeLinks)
                {
                    if (mappedResult.Family != null)
                    {
                        mappedResult.Family = HateoasMainLinks(mappedResult.Family);
                    }                    
                    return Ok(HateoasMainLinks(mappedResult));
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        /// <summary>
        /// Get specific Mammal by Id
        /// </summary>
        /// <remarks>
        /// <h1>Get specific Mammal by HabitatId and you can also include Family and Habitat!</h1>
        /// </remarks>
        [HttpGet("habitatid/{habitatId}")]
        public async Task<ActionResult<MammalDTO>> GetMammalsByHabitatId(int habitatId, [FromQuery] bool includeLinks=true, [FromQuery] bool includeFamily = false, bool includeHabitat = false)
        {
            try
            {
                var results = await _repository.GetMammalsByHabitatId(habitatId, includeFamily, includeHabitat);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();
                if (results == null) throw new System.Exception($"Habitat with {habitatId} does not exist");

                if (includeFamily)
                {
                    foreach (MammalDTO mammal in mappedResult)
                    {
                        mammal.Family.Mammals = null;
                    }
                }

                if (includeLinks)
                {
                    foreach (var mammal in mappedResult)
                    {
                        mammal.Habitats = mammal.Habitats.Select(m => HateoasMainLinks(m)).ToList();
                    }
                    if (includeFamily)
                    {
                        foreach (MammalDTO mammal in mappedResult)
                        {
                            if(mammal.Family != null)
                            {
                                if (!items.ContainsKey(mammal.Family.Name))
                                {
                                    items.Add(mammal.Family.Name, HateoasMainLinks(mammal.Family));
                                }
                                mammal.Family.Mammals = null;
                            }
                        }

                        foreach (MammalDTO mammal in mappedResult)
                        {
                            if (mammal.Family != null)
                            {
                                mammal.Family.Mammals = items[mammal.Family.Name].Mammals;
                            }
                        }
                    }

                    mappedResult = mappedResult.Select(m => HateoasMainLinks(m));
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        /// <summary>
        /// Get Mammal by HabitatName
        /// </summary>
        /// <remarks>
        /// <h1>Get Mammal by HabitatName!</h1>
        /// </remarks>
        [HttpGet("habitat/{habitatName}")]
        public async Task<IActionResult> GetMammalsByHabitatName(string habitatName, [FromQuery] bool includeLinks = true)
        {
            try
            {
                var results = await _repository.GetMammalsByHabitat(habitatName);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                if (results == null) throw new System.Exception($"HabitatName with {habitatName} does not exist");

                if (includeLinks)
                {
                    mappedResult = mappedResult.Select(m => HateoasMainLinks(m));
                }
                
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        /// <summary>
        /// Get Mammals by FamilyId
        /// </summary>
        /// <remarks>
        /// <h1>Get Mammals by FamilyId!</h1>
        /// </remarks>
        [HttpGet("byfamilyid/{id}")]
        public async Task<IActionResult> GetMammalsByFamilyId(int id, [FromQuery] bool includeLinks = true, [FromQuery] bool includeHabitat = false, [FromQuery] bool includeFamily = false)
        {
            try
            {
                var results = await _repository.GetMammalsByFamilyId(id, includeHabitat, includeFamily);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();
                if (results == null) throw new System.Exception($"Family with id{id} does not exist");

                if (includeLinks)
                {
                    foreach (var mammal in mappedResult)
                    {
                        mammal.Habitats = mammal.Habitats.Select(m => HateoasMainLinks(m)).ToList();
                    }

                    foreach (MammalDTO mammal in mappedResult)
                    {
                        if (mammal.Family != null)
                        {
                            if (!items.ContainsKey(mammal.Family.Name))
                            {
                                items.Add(mammal.Family.Name, HateoasMainLinks(mammal.Family));
                            }
                            mammal.Family.Mammals = null;
                        }
                    }

                    foreach (MammalDTO mammal in mappedResult)
                    {
                        if (mammal.Family != null)
                        {
                            mammal.Family.Mammals = items[mammal.Family.Name].Mammals;
                        }
                    }

                    mappedResult = mappedResult.Select(m => HateoasMainLinks(m));
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        /// <summary>
        /// Get Mammal by FamilyName
        /// </summary>
        /// <remarks>
        /// <h1>Get Mammal by FamilyName and you can also include Habitat and Family!</h1>
        /// </remarks>
        [HttpGet("byfamilyname/{familyName}")]
        public async Task<IActionResult> GetMammalsByFamilyName(string familyName, [FromQuery] bool includeLinks = true, [FromQuery] bool includeHabitat = false, [FromQuery] bool includeFamily = false)
        {
            try
            {
                var results = await _repository.GetMammalsByFamily(familyName, includeHabitat, includeFamily);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();
                if (results == null) throw new System.Exception($"FamilyName with {familyName} does not exist");

                if (includeLinks)
                {
                    foreach (var mammal in mappedResult)
                    {
                        mammal.Habitats = mammal.Habitats.Select(m => HateoasMainLinks(m)).ToList();
                    }

                    foreach (MammalDTO mammal in mappedResult)
                    {
                        if (mammal.Family != null)
                        {
                            if (!items.ContainsKey(mammal.Family.Name))
                            {
                                items.Add(mammal.Family.Name, HateoasMainLinks(mammal.Family));
                            }
                            mammal.Family.Mammals = null;
                        }
                    }

                    foreach (MammalDTO mammal in mappedResult)
                    {
                        if (mammal.Family != null)
                        {
                            mammal.Family.Mammals = items[mammal.Family.Name].Mammals;
                        }
                    }                    

                    mappedResult = mappedResult.Select(m => HateoasMainLinks(m));
                }
                
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        /// <summary>
        /// Get Mammal by Lifespan
        /// </summary>
        /// <remarks>
        /// <h1>Get Mammal by Lifespan and you can also include Family and Habitat!</h1>
        /// </remarks>
        [HttpGet("lifespan/fromYear={fromYear}&toYear={toYear}")]
        public async Task<IActionResult> GetMammalsByLifeSpan(int fromYear, int toYear, [FromQuery] bool includeLinks = true, [FromQuery] bool includeFamily = false, [FromQuery] bool includeHabitat = false)
        {
            try
            {
                var results = await _repository.GetMammalsByLifeSpan(fromYear, toYear, includeFamily, includeHabitat);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();
                if (results.Count == 0) throw new System.Exception($"Mammal with lifespan {fromYear} and {toYear} does not exist");

                if (includeLinks)
                {
                    foreach (var mammal in mappedResult)
                    {
                        mammal.Habitats = mammal.Habitats.Select(m => HateoasMainLinks(m)).ToList();
                    }

                    foreach (MammalDTO mammal in mappedResult)
                    {
                        if (mammal.Family != null)
                        {
                            if (!items.ContainsKey(mammal.Family.Name))
                            {
                                items.Add(mammal.Family.Name, HateoasMainLinks(mammal.Family));
                            }
                            mammal.Family.Mammals = null;
                        }
                    }

                    foreach (MammalDTO mammal in mappedResult)
                    {
                        if (mammal.Family != null)
                        {
                            mammal.Family.Mammals = items[mammal.Family.Name].Mammals;
                        }
                    }

                    mappedResult = mappedResult.Select(x => HateoasMainLinks(x)).ToList();
                    return Ok(mappedResult);
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        /// <summary>
        /// Post Mammal
        /// </summary>
        /// <remarks>
        /// <h1>Post Mammal!</h1>
        /// </remarks>
        [ApiKeyAuthentication]
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

        /// <summary>
        /// Put Mammal by Id
        /// </summary>
        /// <remarks>
        /// <h1>Put Mammal by Id!</h1>
        /// </remarks>
        [ApiKeyAuthentication]
        [HttpPut("{mammalId}")]
        public async Task<ActionResult<MammalDTO>> PutMammal (int mammalId, MammalDTO mammalDTO)
        {
            try
            {
                var oldMammal = await _repository.GetMammalById(mammalId);
                if(oldMammal == null)
                {
                    return NotFound($"Mammal with ID: {mammalId} does not exist");
                }

                var newMammal = _mapper.Map(mammalDTO, oldMammal);
                _repository.Update(newMammal);
                if(await _repository.Save())
                {
                    return NoContent();
                }
            }

            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete Mammal by Id
        /// </summary>
        /// <remarks>
        /// <h1>Delete Mammal by Id!</h1>
        /// </remarks>
        [ApiKeyAuthentication]
        [HttpDelete("{mammalId}")]
        public async Task<ActionResult> DeleteMammal(int mammalId)
        {
            try
            {
                var mammalToDelete = await _repository.GetMammalById(mammalId);

                if(mammalToDelete == null)
                {
                    return NotFound($"Mammal with ID: {mammalId} didn't exist");
                }

                _repository.Delete(mammalToDelete);

                if(await _repository.Save())
                {
                    return NoContent();
                }
            }

            catch(TimeoutException e)
            {
                return this.StatusCode(StatusCodes.Status408RequestTimeout, $"Request timeout: {e.Message}");
            }

            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }

            return BadRequest();
        }
    }
}
