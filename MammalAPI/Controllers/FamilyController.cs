using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyController(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetFamilyByName(string name)
        {
            try
            {
                return Ok(await _familyRepository.GetFamilyByName(name));
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

        [HttpGet("byid/{id}", Name = "get-family-id")] 
        public async Task<IActionResult> GetFamilyById(int id)
        {
            try
            {
                return Ok(await _familyRepository.GetFamilyById(id));

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
    }
}
