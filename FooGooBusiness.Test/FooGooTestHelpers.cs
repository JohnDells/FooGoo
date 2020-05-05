using FooGooEf;
using Microsoft.EntityFrameworkCore;
using System;

namespace FooGooBusiness.Test
{
    public static class FooGooTestHelpers
    {
        public static IFooGooDbContext GetDummyContext()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<FooGooContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var context = new FooGooContext(options);
            return context;
        }

        public static FooManager GetFooManager(IFooGooDbContext context)
        {
            var mapper = EfApplicationModule.GetMapper();

            var fooRepository = new FooEfRepository(context, mapper);
            var fooTypeRepository = new FooTypeEfRepository(context, mapper);
            var barRepository = new BarEfRepository(context, mapper);

            return new FooManager(fooRepository, fooTypeRepository, barRepository);
        }

        public static FooEventManager GetFooEventManager(IFooGooDbContext context, IFooManager fooManager)
        {
            var mapper = EfApplicationModule.GetMapper();
            var repository = new FooGooEventEfRepository(context, mapper);
            var serializer = new FooEventJsonSerializationStrategy();
            return new FooEventManager(repository, fooManager, serializer);
        }
    }
}