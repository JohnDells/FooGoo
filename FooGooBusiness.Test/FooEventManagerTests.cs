using FooGooBusiness.Events;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FooGooBusiness.Test
{
    public class FooEventManagerTests
    {
        [Fact]
        public async Task Manager_Should_Create_Foo()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooManagerTests.GetFooManager(context);
            var fooEventManager = new FooEventManager(fooManager);

            var item = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetFooType(item.Id);
            Assert.Equal(item.Name, result.Name);
        }
    }
}