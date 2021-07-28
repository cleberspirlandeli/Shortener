using Alpha.API.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shortener.Common.DTO;
using Shortener.Services.ApplicationService;
using Shortener.Services.Notifications;
using System.Threading.Tasks;

namespace Alpha.API.Controllers.Versions.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShortenController : BaseController
    {
        private readonly ILogger<ShortenController> _logger;
        private readonly IUrlApplicationService _appService;

        public ShortenController(
            IUrlApplicationService appService,
            INotification notifier,
            ILogger<ShortenController> logger) : base(notifier)
        {
            _logger = logger;
            _appService = appService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UrlDto dto)
        {
            var result = await _appService.GenerateShorterUrl(dto);
            return CustomResponse(result);
        }
    }
}
