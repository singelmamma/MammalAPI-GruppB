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

        [HttpGet(Name ="GetAll")]
        public async Task<ActionResult<MammalDTO[]>> Get([FromQuery]bool includeFamily = false, [FromQuery]bool includeHabitat = false)
        {
            try
            {
                var results = await _repository.GetAllMammals(includeFamily, includeHabitat);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                if (includeFamily)
                {
                    foreach(var mammal in mappedResult)
                    {
                        if(mammal.Family != null)
                        {
                            mammal.Family = HateoasMainLinks(mammal.Family);
                        }
                    }
                }
                if (includeHabitat)
                {
                    foreach(var mammal in mappedResult)
                    {
                        mammal.Habitats = mammal.Habitats.Select(m => HateoasMainLinks(m)).ToList();
                    }
                }
                IEnumerable<MammalDTO> mammalsresult = mappedResult.Select(m => HateoasMainLinks(m));
                return Ok(mammalsresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: { e.Message }");
            }
        }

        [HttpGet("{id:int}", Name="GetMammalAsync")]
        public async Task<IActionResult> GetMammalById(int id, [FromQuery] bool includeLinks = true, [FromQuery] bool includeFamily = false, bool includeHabitat = false)
        {
            try
            {
                var result = await _repository.GetMammalById(id, includeFamily, includeHabitat);
                var mappedResult = _mapper.Map<MammalDTO>(result);

                if(includeLinks)
                {
                    mappedResult.Family = HateoasMainLinks(mappedResult.Family);
                    mappedResult.Habitats = mappedResult.Habitats.Select(m => HateoasMainLinks(m)).ToList();
                    return Ok(HateoasMainLinks(mappedResult));
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("{mammalName}", Name="GetMammalName")]
        public async Task<IActionResult> GetMammalByName(string mammalName, [FromQuery] bool includeLinks = true, [FromQuery] bool includeFamilies = false)
        {
            try
            {
                var result = await _repository.GetMammalByName(mammalName, includeFamilies);
                var mappedResult = _mapper.Map<MammalDTO>(result);

                if (includeLinks)
                {
                    mappedResult.Family = HateoasMainLinks(mappedResult.Family);
                    return Ok(HateoasMainLinks(mappedResult));
                }

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("habitatid/{habitatId}")]
        public async Task<IActionResult> GetMammalsByHabitatId(int habitatId, [FromQuery] bool includeFamily = false, bool includeHabitat = false)
        {
            try
            {
                var results = await _repository.GetMammalsByHabitatId(habitatId, includeFamily, includeHabitat);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();


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

                return Ok(mappedResult);
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
                var results = await _repository.GetMammalsByHabitat(habitatName);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                IEnumerable<MammalDTO> mammalsresult = mappedResult.Select(m => HateoasMainLinks(m));
                return Ok(mammalsresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("byfamilyid/{id}")]
        public async Task<IActionResult> GetMammalsByFamilyId(int id, bool includeHabitat = false, bool includeFamily = false)
        {
            try
            {
                var results = await _repository.GetMammalsByFamilyId(id, includeHabitat, includeFamily);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();


                //We have tried filtering in the repositroy but cannot find a good way to limit the recursion depth, hence why we've opted for this approach
                //where we filter the DTO to stop recursion
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
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("byfamilyname/{familyName}")]
        public async Task<IActionResult> GetMammalsByFamilyName(string familyName, bool includeHabitat = false, bool includeFamily = false)
        {
            try
            {
                var results = await _repository.GetMammalsByFamily(familyName, includeHabitat, includeFamily);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();


                //We have tried filtering in the repositroy but cannot find a good way to limit the recursion depth, hence why we've opted for this approach
                //where we filter the DTO to stop recursion
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
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("lifespan/fromYear={fromYear}&toYear={toYear}")]
        public async Task<IActionResult> GetMammalsByLifeSpan(int fromYear, int toYear, [FromQuery] bool includeLinks = true, [FromQuery] bool includeFamily = false, [FromQuery] bool includeHabitat = false)
        {
            try
            {
                var results = await _repository.GetMammalsByLifeSpan(fromYear, toYear, includeFamily, includeHabitat);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                Dictionary<string, FamilyDTO> items = new Dictionary<string, FamilyDTO>();

                if (includeFamily)
                {
                    foreach (MammalDTO mammal in mappedResult)
                    {
                        if (mammal.Family != null)
                        {
                            mammal.Family.Mammals = items[mammal.Family.Name].Mammals;
                        }
                    }
                }

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
