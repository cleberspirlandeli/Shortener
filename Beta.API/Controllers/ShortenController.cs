using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shortener.Services.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Beta.API.Controllers
{
    [ApiController]
    [Route("")]
    public class ShortenController : ControllerBase
    {

        private readonly IUrlApplicationService _appService;
        private readonly ILogger<ShortenController> _logger;

        public ShortenController(IUrlApplicationService appService,
            ILogger<ShortenController> logger)
        {
            _logger = logger;
            _appService = appService;
        }

        [HttpGet("{keyUrl}")]
        public async Task<IActionResult> GetUrlByKey([FromRoute] string keyUrl, CancellationToken cancellationToken = default)
        {

            var result = await _appService.GetUrlByKey(keyUrl);

            if (string.IsNullOrEmpty(result))
            {
                // TODO: Implement CustomResponse();
                return Ok();
            } else
            {
                return Redirect(result);
            }
        }
    }
}
