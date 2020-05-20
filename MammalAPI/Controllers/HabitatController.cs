using MammalAPI.DTO;
using MammalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MammalAPI.HATEOAS;



namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class HabitatController : ControllerBase
    {

        private readonly IHabitatRepository _habitatRepository;
        private readonly IUrlHelper _urlHelper;

        public HabitatController(IHabitatRepository habitatRepository, IUrlHelper injectedUrlHelper)
        {
            _habitatRepository = habitatRepository;
            _urlHelper = injectedUrlHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHabitatByName([FromQuery]string habitatName)
        {
            try
            {
                return Ok(await _habitatRepository.GetHabitatByName(habitatName));
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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabitatById(int id)
        {
            try
            {
                return Ok(await _habitatRepository.GetHabitatById(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllHabitats()
        {
            try
            {
                return Ok(await _habitatRepository.GetAllHabitats());
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //private IEnumerable<Link> CreateLinks(IdNameDTO habitat)
        //{
        //    var links = new[]
        //    {
        //        // new Link
        //        //{
        //        //    Method = "GET",
        //        //    Rel = "self",
        //        //    Href = Url.Link("GetDeliveryById", new {id = habitat.Id})
        //        //},
        //        //new Link
        //        //{
        //        //    Method = "PUT",
        //        //    Rel = "status-delivered",
        //        //    Href = Url.Link("ChangeStatusById", new {id = habitat.Id, status = "delivered"})
        //        //},
        //        //new Link
        //        //{
        //        //    Method = "PATCH",
        //        //    Rel = "status-partial-updated",
        //        //    Href = Url.Link("ChangeStatusById", new {id = habitat.Id, status = "delivered"})
        //        //},
        //        //new Link
        //        //{
        //        //    Method = "DELETE",
        //        //    Rel = "status-deleted",
        //        //    Href = Url.Link("ChangeStatusById", new {id = habitat.Id, status = "delivered"})
        //        //}
        //    };

        //    return links;
        //}

    }
}