using FooGooBusiness.Events;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FooGooBusiness.Test
{
    public class FooEventManagerTests
    {
        [Fact]
        public async Task Manager_Should_Create_FooType()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var item = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetFooType(item.Id);
            Assert.Equal(item.Name, result.Name);
        }

        [Fact]
        public async Task Manager_Should_Update_FooType_Name()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var item = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(item);
            var item2 = new FooTypeUpdateNameEvent { Id = item.Id, Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(item2);

            var result = await fooManager.GetFooType(item2.Id);
            Assert.Equal(item2.Name, result.Name);
        }

        [Fact]
        public async Task Manager_Should_Delete_FooType()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var item = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(item);
            var item2 = new FooTypeDeleteEvent { Id = item.Id };
            await fooEventManager.ProcessFooEventAsync(item2);

            var result = await fooManager.GetFooType(item2.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task Manager_Should_Create_Foo()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var fooType = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType);
            var item = new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooType.Id };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetFoo(item.Id);
            Assert.Equal(item.Name, result.Name);
            Assert.Equal(item.FooTypeId, result.FooTypeId);
        }

        [Fact]
        public async Task Manager_Should_Update_Foo_Name()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var fooType = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType);
            var foo = new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooType.Id };
            await fooEventManager.ProcessFooEventAsync(foo);
            var item = new FooUpdateNameEvent { Id = foo.Id, Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetFoo(item.Id);
            Assert.Equal(item.Name, result.Name);
        }

        [Fact]
        public async Task Manager_Should_Update_Foo_FooTypeId()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var fooType1 = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType1);
            var fooType2 = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType2);
            var foo = new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooType1.Id };
            await fooEventManager.ProcessFooEventAsync(foo);
            var item = new FooUpdateFooTypeIdEvent { Id = foo.Id, FooTypeId = fooType2.Id };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetFoo(item.Id);
            Assert.Equal(item.FooTypeId, result.FooTypeId);
        }

        [Fact]
        public async Task Manager_Should_Delete_Foo()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var fooType = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType);
            var foo = new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooType.Id };
            await fooEventManager.ProcessFooEventAsync(foo);
            var item = new FooDeleteEvent { Id = foo.Id };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetFoo(item.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task Manager_Should_Create_Bar()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var fooType = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType);
            var foo = new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooType.Id };
            await fooEventManager.ProcessFooEventAsync(foo);
            var item = new BarCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooId = foo.Id };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetAllActiveBarsByFooId(foo.Id);
            Assert.Single(result);
            Assert.Equal(item.Name, result[0].Name);
            Assert.Equal(item.FooId, result[0].FooId);
        }

        [Fact]
        public async Task Manager_Should_Update_Bar_Name()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var fooType = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType);
            var foo = new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooType.Id };
            await fooEventManager.ProcessFooEventAsync(foo);
            var bar = new BarCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooId = foo.Id };
            await fooEventManager.ProcessFooEventAsync(bar);
            var item = new BarUpdateNameEvent { Id = bar.Id, Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetAllActiveBarsByFooId(foo.Id);
            Assert.Single(result);
            Assert.Equal(item.Name, result[0].Name);
        }

        [Fact]
        public async Task Manager_Should_Delete_Bar()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var fooEventManager = FooGooTestHelpers.GetFooEventManager(context, fooManager);

            var fooType = new FooTypeCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
            await fooEventManager.ProcessFooEventAsync(fooType);
            var foo = new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooType.Id };
            await fooEventManager.ProcessFooEventAsync(foo);
            var bar = new BarCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooId = foo.Id };
            await fooEventManager.ProcessFooEventAsync(bar);
            var item = new BarDeleteEvent { Id = bar.Id };
            await fooEventManager.ProcessFooEventAsync(item);

            var result = await fooManager.GetAllActiveBarsByFooId(foo.Id);
            Assert.Empty(result);
        }
    }
}