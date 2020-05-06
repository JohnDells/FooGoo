using System;

namespace FooGooBusiness.Events
{
    public class FooUpdateNameEvent : IFooGooEvent
    {
        public string EventType => FooEventConstants.UpdateFooName;

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}