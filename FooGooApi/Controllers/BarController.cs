using FooGooBusiness;
using FooGooBusiness.Bars;
using FooGooBusiness.Foos;
using FooGooBusiness.FooTypes;
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

        public BarController(ILogger<FooTypeController> logger, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:MongoDbDefault"];
            var fooRepository = new FooMongoDbRepository(connectionString);
            var fooTypeRepository = new FooTypeMongoDbRepository(connectionString);
            var barRepository = new BarMongoDbRepository(connectionString);
            _manager = new FooManager(fooRepository, fooTypeRepository, barRepository);
            _logger = logger;
        }

        [HttpGet]
        [Route("api/foos/{fooId}/bars")]
        public async Task<IEnumerable<Bar>> GetAll(Guid fooId)
        {
            return await _manager.GetAllActiveBarsByFooId(fooId);
        }

        [HttpPost]
        [Route("api/foos/{fooId}/bars/{name}")]
        public async Task Insert(Guid fooId, string name)
        {
            await _manager.InsertBar(fooId, name);
        }

        [HttpPut]
        [Route("api/foos/{fooId}/bars/{id}/name/{name}")]
        public async Task UpdateName(Guid id, string name)
        {
            await _manager.UpdateBarName(id, name);
        }
    }
}
