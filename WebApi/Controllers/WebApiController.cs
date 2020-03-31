using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowedOrigin")]

    [Route("[controller]")]
    public class WebApiController : ControllerBase
    {
     
        private readonly ILogger<WebApiController> _logger;

        public WebApiController(ILogger<WebApiController> logger)
        {
            _logger = logger;
        }

    }
}
