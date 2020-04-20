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
    public class FooTypeController : ControllerBase
    {
        private readonly IFooManager _manager;
        private readonly ILogger<FooTypeController> _logger;

        public FooTypeController(ILogger<FooTypeController> logger, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:MongoDbDefault"];
            var fooRepository = new FooMongoDbRepository(connectionString);
            var fooTypeRepository = new FooTypeMongoDbRepository(connectionString);
            var barRepository = new BarMongoDbRepository(connectionString);
            _manager = new FooManager(fooRepository, fooTypeRepository, barRepository);
            _logger = logger;
        }

        [HttpGet]
        [Route("api/footypes")]
        public async Task<IEnumerable<FooType>> GetAll()
        {
            return await _manager.GetAllActiveFooTypes();
        }

        [HttpPost]
        [Route("api/footypes/{name}")]
        public async Task Insert(string name)
        {
            await _manager.InsertFooType(name);
        }

        [HttpPut]
        [Route("api/footypes/{id}/name/{name}")]
        public async Task UpdateName(Guid id, string name)
        {
            await _manager.UpdateFooTypeName(id, name);
        }
    }
}
