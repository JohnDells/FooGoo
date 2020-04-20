﻿using FooGooBusiness;
using FooGooBusiness.Bars;
using FooGooBusiness.Foos;
using FooGooBusiness.FooTypes;
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

        public FooController(ILogger<FooController> logger, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:MongoDbDefault"];
            var fooRepository = new FooMongoDbRepository(connectionString);
            var fooTypeRepository = new FooTypeMongoDbRepository(connectionString);
            var barRepository = new BarMongoDbRepository(connectionString);
            _manager = new FooManager(fooRepository, fooTypeRepository, barRepository);
            _logger = logger;
        }

        [HttpGet]
        [Route("api/foos")]
        public async Task<IEnumerable<Foo>> GetAll()
        {
            return await _manager.GetAllActiveFoos();
        }

        [HttpGet]
        [Route("api/foos/fooTypeId/{fooTypeId}")]
        public async Task<List<Foo>> GetActiveByType(Guid fooTypeId)
        {
            return await _manager.GetAllActiveFoosByType(fooTypeId);
        }

        [HttpGet]
        [Route("api/foos/{id}")]
        public async Task<Foo> Get(Guid id)
        {
            return await _manager.GetFoo(id);
        }

        [HttpPost]
        [Route("api/foos/{name}")]
        public async Task Insert(string name)
        {
            await _manager.InsertFoo(name);
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