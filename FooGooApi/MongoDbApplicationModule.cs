using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using FooGooBusiness;
using FooGooBusiness.Bars;
using FooGooBusiness.Foos;
using FooGooBusiness.FooTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooApi
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
            builder.Register(c => new FooTypeMongoDbRepository(_connectionString)).As<IFooTypeRepository>();
            builder.Register(c => new FooMongoDbRepository(_connectionString)).As<IFooRespository>();
            builder.Register(c => new BarMongoDbRepository(_connectionString)).As<IBarRepository>();

            builder.RegisterType<FooManager>().As<IFooManager>();

            base.Load(builder);
        }
    }
}