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
        public async Task<IEnumerable<FooDto>> GetAll()
        {
            return await _manager.GetAllActiveFoos();
        }

        [HttpGet]
        [Route("api/foos/fooTypeId/{fooTypeId}")]
        public async Task<List<FooDto>> GetActiveByType(Guid fooTypeId)
        {
            return await _manager.GetAllActiveFoosByType(fooTypeId);
        }

        [HttpGet]
        [Route("api/foos/{id}")]
        public async Task<FooDto> Get(Guid id)
        {
            return await _manager.GetFoo(id);
        }

        [HttpPost]
        [Route("api/foos")]
        public async Task Insert([FromBody] FooDto item)
        {
            await _manager.InsertFoo(item);
        }

        [HttpPut]
        [Route("api/foos/{id}/name/{name}")]
        public async Task UpdateName(Guid id, string name)
        {
            await _manager.UpdateFooName(id, name);
        }

        [HttpPut]
        [Route("api/foos/{id}/fooTypeId/{fooTypeId}")]
        public async Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            await _manager.UpdateFooTypeId(id, fooTypeId);
        }

        [HttpPut]
        [Route("api/foos/{id}/deactivate")]
        public async Task Deactivate(Guid id)
        {
            await _manager.DeactivateFoo(id);
        }
    }
}