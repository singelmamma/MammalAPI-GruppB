using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammalAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class MammalController : ControllerBase
    {
        // Need to DI repository into controller to be used to call the "GetAllMammals" method
        // Need to initialize 
        public MammalController()
        {

        }

        // Need to return Ok and await service that is not yet created.
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return null;
        }
    }
}
