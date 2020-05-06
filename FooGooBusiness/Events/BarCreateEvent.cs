using System;

namespace FooGooBusiness.Events
{
    public class BarCreateEvent : IFooGooEvent
    {
        public string EventType => FooEventConstants.CreateBar;

        public Guid Id { get; set; }

        public Guid FooId { get; set; }

        public string Name { get; set; }
    }
}