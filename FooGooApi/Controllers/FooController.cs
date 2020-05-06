using FooGooBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooApi.Controllers
{
    [ApiController]
    public class FooController : ControllerBase
    {
        private readonly IFooManager _manager;
        private readonly ILogger<FooController> _logger;

        public FooController(IFooManager manager, ILogger<FooController> logger, IConfiguration configuration)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/foos")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _manager.GetAllActiveFoos();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/foos/fooTypeId/{fooTypeId}")]
        public async Task<IActionResult> GetActiveByType(Guid fooTypeId)
        {
            var result = await _manager.GetAllActiveFoosByType(fooTypeId);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/foos/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _manager.GetFoo(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/foos")]
        public async Task<IActionResult> Create([FromBody] FooDto item)
        {
            await _manager.CreateFoo(item);
            return Ok();
        }

        [HttpPut]
        [Route("api/foos/{id}/name/{name}")]
        public async Task<IActionResult> UpdateName(Guid id, string name)
        {
            await _manager.UpdateFooName(id, name);
            return Ok();
        }

        [HttpPut]
        [Route("api/foos/{id}/fooTypeId/{fooTypeId}")]
        public async Task<IActionResult> UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            await _manager.UpdateFooTypeId(id, fooTypeId);
            return Ok();
        }

        [HttpPut]
        [Route("api/foos/{id}/deactivate")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _manager.DeleteFoo(id);
            return Ok();
        }
    }
}