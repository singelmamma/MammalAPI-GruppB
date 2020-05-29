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
    public class MammalsController : HateoasMammalControllerBase
    {
        private readonly IMammalRepository _repository;
        private readonly IMapper _mapper;

        public MammalsController(IMammalRepository repository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("family/{includeFamily}/habitat{includeHabitat}",Name ="GetAll")]
        public async Task<ActionResult<MammalDTO[]>> Get(bool includeFamily = false, bool includeHabitat = false)
        {
            try
            {
                var results = await _repository.GetAllMammals(includeFamily, includeHabitat);
                IEnumerable<MammalDTO> mappedResult = _mapper.Map<MammalDTO[]>(results);
                IEnumerable<MammalDTO> mammalsresult = mappedResult.Select(m => HateoasMainLinks(m));

                return Ok(mammalsresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: { e.Message }");
            }
        }

        [HttpGet("{mammalName}")]
        public async Task<IActionResult> GetMammalByName(string mammalName, bool includeFamilies = false)
        {
            try
            {
                var result = await _repository.GetMammalByName(mammalName, includeFamilies);
                var mappedResult = _mapper.Map<List<MammalDTO>>(result);

                if(includeFamilies)
                {
                    foreach(MammalDTO mammal in mappedResult)
                    {    
                            mammal.Family.Mammals = null;
                    }
                }
                
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, $"Something went wrong: { e.Message }");
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

        [HttpGet("habitatId={habitatId}")]
        public async Task<IActionResult> GetMammalsByHabitatId(int habitatId, bool includeFamilies = false)
        {
            try
            {
                var result = await _repository.GetMammalsByHabitatId(habitatId, includeFamilies);
                var mappedResult = _mapper.Map<List<MammalDTO>>(result);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Something went wrong: { e.Message }");
            }
        }

        [HttpGet("lifespan/fromYear={fromYear}&toYear={toYear}")]
        public async Task<IActionResult> GetMammalByLifeSpan(int fromYear, int toYear, bool includeFamily = false, bool includeHabitat = false)
        {
            try
            {
                var result= await _repository.GetMammalsByLifeSpan(fromYear, toYear, includeFamily, includeHabitat);
                var mappedResult = _mapper.Map<List<MammalDTO>>(result);
                if(includeHabitat)
                {
                    foreach(MammalDTO mammal in mappedResult)
                    {
                        foreach(HabitatDTO habitat in mammal.Habitats)
                        {
                            habitat.Mammal = null;
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
                var result = await _repository.GetMammalsByFamily(familyName, includeHabitat, includeFamily);
                var mappedResult = _mapper.Map<List<MammalDTO>>(result);

                //We have tried filtering in the repositroy but cannot find a good way to limit the recursion depth, hence why we've opted for this approach
                //where we filter the DTO to stop recursion
                if (includeHabitat)
                {
                    foreach (MammalDTO mammal in mappedResult)
                    {
                        foreach (HabitatDTO habitat in mammal.Habitats)
                        {
                            habitat.Mammal = null;
                        }
                    }                    
                }

                if (includeFamily)
                {
                    foreach (MammalDTO mammal in mappedResult)
                    {
                        mammal.Family.Mammals = null;
                    }
                }
                                return Ok(mappedResult);
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
                var result= await _repository.GetMammalsByFamilyId(id, includeHabitat, includeFamily);
                var mappedResult = _mapper.Map<List<MammalDTO>>(result);

                //We have tried filtering in the repositroy but cannot find a good way to limit the recursion depth, hence why we've opted for this approach
                //where we filter the DTO to stop recursion
                if (includeHabitat)
                {
                    foreach (MammalDTO mammal in mappedResult)
                    {
                        foreach (HabitatDTO habitat in mammal.Habitats)
                        {
                            habitat.Mammal = null;
                        }
                    }
                }

                if (includeFamily)
                {
                    foreach (MammalDTO mammal in mappedResult)
                    {
                        mammal.Family.Mammals = null;
                    }
                }
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
