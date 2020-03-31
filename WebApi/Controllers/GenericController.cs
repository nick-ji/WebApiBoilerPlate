using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Serilog;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GenericController> _logger;

        public GenericController(IConfiguration configuration, ILogger<GenericController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            Log.Information("ping-pong!");
            var result = new JObject();
            result["message"] = "pong";

            return Ok(result);
        }
    }
}