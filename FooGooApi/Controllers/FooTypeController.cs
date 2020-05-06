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
    public class FooTypeController : ControllerBase
    {
        private readonly IFooManager _manager;
        private readonly ILogger<FooTypeController> _logger;

        public FooTypeController(IFooManager manager, ILogger<FooTypeController> logger, IConfiguration configuration)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/footypes")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _manager.GetAllActiveFooTypes();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/footypes/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _manager.GetFooType(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/footypes")]
        public async Task<IActionResult> Create([FromBody] FooTypeDto item)
        {
            await _manager.CreateFooType(item);
            return Ok();
        }

        [HttpPut]
        [Route("api/footypes/{id}/name/{name}")]
        public async Task<IActionResult> UpdateName(Guid id, string name)
        {
            await _manager.UpdateFooTypeName(id, name);
            return Ok();
        }

        [HttpPut]
        [Route("api/footypes/{id}/deactivate")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _manager.DeleteFooType(id);
            return Ok();
        }
    }
}