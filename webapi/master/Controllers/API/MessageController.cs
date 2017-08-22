using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Nasdan.Core.Senses;

namespace Nasdan.Controllers.Web
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            return StatusCode(201);
        }
        /* 
        [HttpPost]
        public IActionResult StringMessage([FromBody] StringMessage msg)
        {
            if (msg == null)
            {
                return BadRequest();
            }
            Nasdan.Core.API.Nasdan.Tell(msg);
            return StatusCode(201);
        }
        */

        [HttpPost]
        public IActionResult ImageMessage(ImageMessage msg)
        {
            if (msg == null)
            {
                return BadRequest();
            }
            Nasdan.Core.API.Nasdan.Tell(msg);            
            return StatusCode(201);
        }

        
    }
}