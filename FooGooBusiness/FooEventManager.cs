using FooGooBusiness.Events;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public class FooEventManager
    {
        private readonly IFooManager _manager;

        public FooEventManager(IFooManager manager)
        {
            _manager = manager;
        }

        public async Task ProcessFooEventAsync(IFooEvent item)
        {
            switch (item.Type)
            {
                case FooEventConstants.CreateFooType:
                    var e1 = (FooTypeCreateEvent)item;
                    await _manager.CreateFooType(new FooTypeDto { FooTypeId = e1.Id, Name = e1.Name, Active = true });
                    break;
            }
        }
    }
}