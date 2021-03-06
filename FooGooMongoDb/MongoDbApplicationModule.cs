﻿using Autofac;
using AutoMapper;
using FooGooBusiness;
using MongoDB.Driver;

namespace FooGooMongoDb
{
    public class MongoDbApplicationModule : Module
    {
        private readonly string _connectionString;

        public MongoDbApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var client = new MongoClient(_connectionString);
            var mapper = GetMapper();

            builder.Register(c => new FooTypeMongoDbRepository(client, mapper)).As<IFooTypeRepository>();
            builder.Register(c => new FooMongoDbRepository(client, mapper)).As<IFooRespository>();
            builder.Register(c => new BarMongoDbRepository(client, mapper)).As<IBarRepository>();

            base.Load(builder);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FooDoc, FooDto>();
                cfg.CreateMap<FooDto, FooDoc>();
                cfg.CreateMap<FooTypeDoc, FooTypeDto>();
                cfg.CreateMap<FooTypeDto, FooTypeDoc>();
                cfg.CreateMap<BarDoc, BarDto>();
                cfg.CreateMap<BarDto, BarDoc>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}