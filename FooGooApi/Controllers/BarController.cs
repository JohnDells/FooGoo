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
    public class BarController : ControllerBase
    {
        private readonly IFooManager _manager;
        private readonly ILogger<FooTypeController> _logger;

        public BarController(IFooManager manager, ILogger<FooTypeController> logger, IConfiguration configuration)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/foos/{fooId}/bars")]
        public async Task<IActionResult> GetAll(Guid fooId)
        {
            var result = await _manager.GetAllActiveBarsByFooId(fooId);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/foos/{fooId}/bars")]
        public async Task<IActionResult> Create([FromBody] BarDto item)
        {
            await _manager.CreateBar(item);
            return Ok();
        }

        [HttpPut]
        [Route("api/foos/{fooId}/bars/{id}/name/{name}")]
        public async Task<IActionResult> UpdateName(Guid id, string name)
        {
            await _manager.UpdateBarName(id, name);
            return Ok();
        }

        [HttpPut]
        [Route("api/foos/{fooId}/bars/{id}/deactivate")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _manager.DeleteBar(id);
            return Ok();
        }
    }
}
