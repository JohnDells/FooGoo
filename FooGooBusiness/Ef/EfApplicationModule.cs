using Autofac;
using AutoMapper;

namespace FooGooBusiness.Ef
{
    public class EfApplicationModule : Module
    {
        private readonly string _connectionString;

        public EfApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var mapper = GetMapper();

            builder.Register(c => new FooTypeEfRepository(_connectionString, mapper)).As<IFooTypeRepository>();
            builder.Register(c => new FooEfRepository(_connectionString, mapper)).As<IFooRespository>();
            builder.Register(c => new BarEfRepository(_connectionString, mapper)).As<IBarRepository>();

            builder.RegisterType<FooManager>().As<IFooManager>();

            base.Load(builder);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FooEntity, FooDto>();
                cfg.CreateMap<FooTypeEntity, FooTypeDto>();
                cfg.CreateMap<BarEntity, BarDto>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}