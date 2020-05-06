using FooGooBusiness.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public class FooEventManager : IFooEventManager
    {
        private readonly IFooGooEventRepository _repository;
        private readonly IFooManager _manager;
        private readonly IFooEventSerializationStrategy _serializer;

        public FooEventManager(IFooGooEventRepository repository, IFooManager manager, IFooEventSerializationStrategy serializer)
        {
            _repository = repository;
            _manager = manager;
            _serializer = serializer;
        }

        public async Task AddEvents(FooGooEventsDto items, Guid userId)
        {
            await AddEvents(items.SubEvents, userId, items.CorrelationId);
        }

        public async Task AddEvents(List<IFooGooEvent> events, Guid userId, Guid? correlationId = null)
        {
            var temp = Map(events, userId, correlationId);
            await _repository.AddFooGooEvents(temp);
        }

        private List<FooGooEventDto> Map(List<IFooGooEvent> events, Guid userId, Guid? correlationId = null)
        {
            var result = new List<FooGooEventDto>();

            var now = DateTime.Now;
            foreach (var subEvent in events)
            {
                var value = _serializer.Serialize(subEvent);
                var item = new FooGooEventDto
                {
                    CorrelationId = correlationId,
                    Type = subEvent.EventType,
                    CreatedDate = now,
                    CreatedBy = userId,
                    Value = value
                };
                result.Add(item);
            }

            return result;
        }

        public async Task Process(Guid userId)
        {
            var currentSequenceId = await _repository.GetLiveSequenceId();

            var newSequenceId = 0L;
            var newEvents = await _repository.Get(currentSequenceId);
            foreach (var newEvent in newEvents)
            {
                var item = _serializer.Deserialize(newEvent.Value);
                await ProcessFooEventAsync(item);
                newSequenceId = newEvent.SequenceId;
            }

            await _repository.SetLiveSequenceId(newSequenceId, userId);
        }

        public async Task ProcessFooEventAsync(IFooGooEvent item)
        {
            switch (item.EventType)
            {
                case FooEventConstants.CreateFooType:
                    var e1 = (FooTypeCreateEvent)item;
                    await _manager.CreateFooType(new FooTypeDto { FooTypeId = e1.Id, Name = e1.Name, Active = true });
                    break;

                case FooEventConstants.UpdateFooTypeName:
                    var e2 = (FooTypeUpdateNameEvent)item;
                    await _manager.UpdateFooTypeName(e2.Id, e2.Name);
                    break;

                case FooEventConstants.DeleteFooType:
                    var e3 = (FooTypeDeleteEvent)item;
                    await _manager.DeleteFooType(e3.Id);
                    break;

                case FooEventConstants.CreateFoo:
                    var e4 = (FooCreateEvent)item;
                    await _manager.CreateFoo(new FooDto { FooId = e4.Id, FooTypeId = e4.FooTypeId, Name = e4.Name, Active = true });
                    break;

                case FooEventConstants.UpdateFooName:
                    var e5 = (FooUpdateNameEvent)item;
                    await _manager.UpdateFooName(e5.Id, e5.Name);
                    break;

                case FooEventConstants.UpdateFooFooTypeId:
                    var e6 = (FooUpdateFooTypeIdEvent)item;
                    await _manager.UpdateFooTypeId(e6.Id, e6.FooTypeId);
                    break;

                case FooEventConstants.DeleteFoo:
                    var e7 = (FooDeleteEvent)item;
                    await _manager.DeleteFoo(e7.Id);
                    break;

                case FooEventConstants.CreateBar:
                    var e8 = (BarCreateEvent)item;
                    await _manager.CreateBar(new BarDto { BarId = e8.Id, FooId = e8.FooId, Name = e8.Name, Active = true });
                    break;

                case FooEventConstants.UpdateBarName:
                    var e9 = (BarUpdateNameEvent)item;
                    await _manager.UpdateBarName(e9.Id, e9.Name);
                    break;

                case FooEventConstants.DeleteBar:
                    var e10 = (BarDeleteEvent)item;
                    await _manager.DeleteBar(e10.Id);
                    break;
            }
        }
    }
}