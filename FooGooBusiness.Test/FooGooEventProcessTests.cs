using FooGooBusiness.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FooGooBusiness.Test
{
    public class FooGooEventProcessTests
    {
        [Fact]
        public async Task Process_Should_Create_FooType()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId = Guid.NewGuid();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            Assert.Equal(2, context.FooGooEvents.Count());
        }

        [Fact]
        public async Task Process_Should_Process_CreateFooTypeEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId = Guid.NewGuid();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId, Name = Guid.NewGuid().ToString() }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            Assert.Equal(1, context.FooTypes.Count());
            Assert.Equal(fooTypeId, context.FooTypes.Select(x => x.FooTypeId).FirstOrDefault());
        }

        [Fact]
        public async Task Process_Should_Process_UpdateFooTypeNameEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId, Name = Guid.NewGuid().ToString() },
                new FooTypeUpdateNameEvent { Id = fooTypeId, Name = name }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            Assert.Equal(name, context.FooTypes.Select(x => x.Name).FirstOrDefault());
        }

        [Fact]
        public async Task Process_Should_Process_DeleteFooTypeEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId, Name = Guid.NewGuid().ToString() },
                new FooTypeDeleteEvent { Id = fooTypeId }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            Assert.Empty(await fooManager.GetAllActiveFooTypes());
        }


        [Fact]
        public async Task Process_Should_Process_CreateFooEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId = Guid.NewGuid();
            var fooId = Guid.NewGuid();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = fooId, Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            Assert.Equal(1, context.Foos.Count());
            Assert.Equal(events[1].Id, context.Foos.Select(x => x.FooId).FirstOrDefault());
        }

        [Fact]
        public async Task Process_Should_Process_UpdateFooNameEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId = Guid.NewGuid();
            var fooId = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = fooId, Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId },
                new FooUpdateNameEvent { Id = fooId, Name = name }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            Assert.Equal(1, context.Foos.Count());
            Assert.Equal(name, context.Foos.Select(x => x.Name).FirstOrDefault());
        }

        [Fact]
        public async Task Process_Should_Process_UpdateFooFooTypeEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId1 = Guid.NewGuid();
            var fooTypeId2 = Guid.NewGuid();
            var fooId = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId1, Name = Guid.NewGuid().ToString() },
                new FooTypeCreateEvent { Id = fooTypeId2, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = fooId, Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId1 },
                new FooUpdateFooTypeIdEvent { Id = fooId, FooTypeId = fooTypeId2 }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            Assert.Equal(1, context.Foos.Count());
            Assert.Equal(fooTypeId2, context.Foos.Select(x => x.FooTypeId).FirstOrDefault());
        }

        [Fact]
        public async Task Process_Should_Process_DeleteFooEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId1 = Guid.NewGuid();
            var fooId = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId1, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = fooId, Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId1 },
                new FooDeleteEvent { Id = fooId }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            Assert.Empty(await fooManager.GetAllActiveFoos());
        }

        [Fact]
        public async Task Process_Should_Process_CreateBarEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId1 = Guid.NewGuid();
            var fooId = Guid.NewGuid();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId1, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = fooId, Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId1 },
                new BarCreateEvent { Id = Guid.NewGuid(), FooId = fooId, Name = Guid.NewGuid().ToString() }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            var bars = await fooManager.GetAllActiveBarsByFooId(fooId);
            Assert.Single(bars);
            Assert.Equal(fooId, bars[0].FooId);
        }

        [Fact]
        public async Task Process_Should_Process_UpdateBarNameEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId1 = Guid.NewGuid();
            var fooId = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId1, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = fooId, Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId1 },
                new BarCreateEvent { Id = barId, FooId = fooId, Name = Guid.NewGuid().ToString() },
                new BarUpdateNameEvent { Id = barId, Name = name }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            var bars = await fooManager.GetAllActiveBarsByFooId(fooId);
            Assert.Single(bars);
            Assert.Equal(name, bars[0].Name);
        }

        [Fact]
        public async Task Process_Should_Process_DeleteBarEvent()
        {
            var context = FooGooTestHelpers.GetDummyContext();
            var fooManager = FooGooTestHelpers.GetFooManager(context);
            var manager = FooGooTestHelpers.GetFooEventManager(context, fooManager);
            var fooTypeId1 = Guid.NewGuid();
            var fooId = Guid.NewGuid();
            var barId = Guid.NewGuid();
            var events = new List<IFooGooEvent>
            {
                new FooTypeCreateEvent { Id = fooTypeId1, Name = Guid.NewGuid().ToString() },
                new FooCreateEvent { Id = fooId, Name = Guid.NewGuid().ToString(), FooTypeId = fooTypeId1 },
                new BarCreateEvent { Id = barId, FooId = fooId, Name = Guid.NewGuid().ToString() },
                new BarDeleteEvent { Id = barId }
            };
            await manager.AddEvents(events, Guid.NewGuid());

            await manager.Process(Guid.NewGuid());

            var bars = await fooManager.GetAllActiveBarsByFooId(fooId);
            Assert.Empty(bars);
        }
    }
}