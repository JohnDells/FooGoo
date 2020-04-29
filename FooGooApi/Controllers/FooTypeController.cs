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
        public async Task<IEnumerable<FooTypeDto>> GetAll()
        {
            return await _manager.GetAllActiveFooTypes();
        }

        [HttpGet]
        [Route("api/footypes/{id}")]
        public async Task<FooTypeDto> Get(Guid id)
        {
            return await _manager.GetFooType(id);
        }

        [HttpPost]
        [Route("api/footypes")]
        public async Task Create([FromBody] FooTypeDto item)
        {
            await _manager.CreateFooType(item);
        }

        [HttpPut]
        [Route("api/footypes/{id}/name/{name}")]
        public async Task UpdateName(Guid id, string name)
        {
            await _manager.UpdateFooTypeName(id, name);
        }

        [HttpPut]
        [Route("api/footypes/{id}/deactivate")]
        public async Task Delete(Guid id)
        {
            await _manager.DeleteFooType(id);
        }
    }
}