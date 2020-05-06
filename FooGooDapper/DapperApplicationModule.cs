using Autofac;
using AutoMapper;
using FooGooBusiness;

namespace FooGooDapper
{
    public class DapperApplicationModule : Module
    {
        private readonly string _connectionString;

        public DapperApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var mapper = GetMapper();

            builder.Register(c => new FooTypeDapperRespository(_connectionString, mapper)).As<IFooTypeRepository>();
            builder.Register(c => new FooDapperRepository(_connectionString, mapper)).As<IFooRespository>();
            builder.Register(c => new BarDapperRepository(_connectionString, mapper)).As<IBarRepository>();
            builder.Register(c => new FooGooEventDapperRepository(_connectionString, mapper)).As<IFooGooEventRepository>();

            base.Load(builder);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FooRec, FooDto>();
                cfg.CreateMap<FooDto, FooRec>();
                cfg.CreateMap<FooTypeRec, FooTypeDto>();
                cfg.CreateMap<FooTypeDto, FooTypeRec>();
                cfg.CreateMap<BarRec, BarDto>();
                cfg.CreateMap<BarDto, BarRec>();
                cfg.CreateMap<FooGooEventRec, FooGooEventDto>();
                cfg.CreateMap<FooGooEventDto, FooGooEventRec>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}