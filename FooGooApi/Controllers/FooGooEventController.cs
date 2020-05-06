using FooGooBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooApi.Controllers
{
    [ApiController]
    public class FooGooEventController : ControllerBase
    {
        private readonly IFooEventManager _manager;
        private readonly ILogger<FooGooEventController> _logger;

        public FooGooEventController(IFooEventManager manager, ILogger<FooGooEventController> logger, IConfiguration configuration)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/foogooevents")]
        public async Task<IActionResult> Add(FooGooEventsDto items)
        {
            //  TODO:  Replace with identity.
            var userId = Guid.NewGuid();
            await _manager.AddEvents(items, userId);
            return Ok();
        }

        [HttpPost]
        [Route("api/foogooevents/process")]
        public async Task<IActionResult> Process()
        {
            var userId = Guid.NewGuid();
            await _manager.Process(userId);
            return Ok();
        }

    }
}
