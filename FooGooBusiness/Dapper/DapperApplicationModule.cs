using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooGooBusiness.Dapper
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
            builder.Register(c => new FooTypeDapperRespository(_connectionString)).As<IFooTypeRepository>();
            builder.Register(c => new FooDapperRepository(_connectionString)).As<IFooRespository>();
            builder.Register(c => new BarDapperRepository(_connectionString)).As<IBarRepository>();

            builder.RegisterType<FooManager>().As<IFooManager>();

            base.Load(builder);
        }
    }
}
