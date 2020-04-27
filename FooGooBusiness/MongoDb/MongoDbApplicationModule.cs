using Autofac;
using AutoMapper;

namespace FooGooBusiness.MongoDb
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
            var mapper = GetMapper();

            builder.Register(c => new FooTypeMongoDbRepository(_connectionString, mapper)).As<IFooTypeRepository>();
            builder.Register(c => new FooMongoDbRepository(_connectionString, mapper)).As<IFooRespository>();
            builder.Register(c => new BarMongoDbRepository(_connectionString, mapper)).As<IBarRepository>();

            builder.RegisterType<FooManager>().As<IFooManager>();

            base.Load(builder);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FooDoc, FooDto>();
                cfg.CreateMap<FooTypeDoc, FooTypeDto>();
                cfg.CreateMap<BarDoc, BarDto>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}