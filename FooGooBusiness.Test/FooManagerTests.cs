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
            var manager = FooGooTestHelpers.GetFooManager(context);

            var item = new FooTypeDto { FooTypeId = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), Active = true };
            await manager.CreateFooType(item);

            var result = await manager.GetFooType(item.FooTypeId);
            Assert.Equal(item.Name, result.Name);
        }
    }
}