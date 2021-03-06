﻿using Autofac;
using AutoMapper;
using FooGooBusiness;

namespace FooGooEf
{
    /// <summary>
    /// Configures EF context and the DTO/Entity mapping for each repository.
    /// </summary>
    public class EfApplicationModule : Module
    {
        private readonly string _connectionString;

        public EfApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var context = new FooGooContext(_connectionString);
            var mapper = GetMapper();

            builder.Register(c => new FooTypeEfRepository(context, mapper)).As<IFooTypeRepository>();
            builder.Register(c => new FooEfRepository(context, mapper)).As<IFooRespository>();
            builder.Register(c => new BarEfRepository(context, mapper)).As<IBarRepository>();
            builder.Register(c => new FooGooEventEfRepository(context, mapper)).As<IFooGooEventRepository>();

            base.Load(builder);
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FooEntity, FooDto>();
                cfg.CreateMap<FooDto, FooEntity>();
                cfg.CreateMap<FooTypeEntity, FooTypeDto>();
                cfg.CreateMap<FooTypeDto, FooTypeEntity>();
                cfg.CreateMap<BarEntity, BarDto>();
                cfg.CreateMap<BarDto, BarEntity>();
                cfg.CreateMap<FooGooEventDto, FooGooEventEntity>();
                cfg.CreateMap<FooGooEventEntity, FooGooEventDto>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}