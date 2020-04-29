using FooGooEf;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FooGooBusiness.Test
{
    public class FooManagerTests
    {
        [Fact]
        public async Task Manager_Should_Create_FooType()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var manager = GetFooManager(context);

            var item = new FooTypeDto { FooTypeId = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), Active = true };
            await manager.CreateFooType(item);

            var result = await manager.GetFooType(item.FooTypeId);
            Assert.Equal(item.Name, result.Name);
        }

        public static FooManager GetFooManager(FooGooContext context)
        {
            var mapper = EfApplicationModule.GetMapper();

            var fooRepository = new FooEfRepository(context, mapper);
            var fooTypeRepository = new FooTypeEfRepository(context, mapper);
            var barRepository = new BarEfRepository(context, mapper);

            return new FooManager(fooRepository, fooTypeRepository, barRepository);
        }
    }
}