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
        public async Task<IEnumerable<BarDto>> GetAll(Guid fooId)
        {
            return await _manager.GetAllActiveBarsByFooId(fooId);
        }

        [HttpPost]
        [Route("api/foos/{fooId}/bars")]
        public async Task Create([FromBody] BarDto item)
        {
            await _manager.CreateBar(item);
        }

        [HttpPut]
        [Route("api/foos/{fooId}/bars/{id}/name/{name}")]
        public async Task UpdateName(Guid id, string name)
        {
            await _manager.UpdateBarName(id, name);
        }

        [HttpPut]
        [Route("api/foos/{fooId}/bars/{id}/deactivate")]
        public async Task Delete(Guid id)
        {
            await _manager.DeleteBar(id);
        }
    }
}
