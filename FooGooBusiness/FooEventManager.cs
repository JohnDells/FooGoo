using FooGooBusiness.Events;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public class FooEventManager : IFooEventManager
    {
        private readonly IFooManager _manager;

        public FooEventManager(IFooManager manager)
        {
            _manager = manager;
        }

        public async Task ProcessFooEventAsync(IFooGooEvent item)
        {
            switch (item.Type)
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
                    await _manager.UpdateFooTypeName(e5.Id, e5.Name);
                    break;
                case FooEventConstants.UpdateFooFooTypeId:
                    var e6 = (FooUpdateFooTypeIdEvent)item;
                    await _manager.UpdateFooTypeId(e6.Id, e6.FooTypeId);
                    break;
                case FooEventConstants.DeleteFoo:
                    var e7 = (FooTypeDeleteEvent)item;
                    await _manager.DeleteFooType(e7.Id);
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