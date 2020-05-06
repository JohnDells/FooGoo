using System;

namespace FooGooBusiness.Events
{
    public class BarUpdateNameEvent : IFooGooEvent
    {
        public string EventType => FooEventConstants.UpdateBarName;

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}