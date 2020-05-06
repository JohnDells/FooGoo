using System;

namespace FooGooBusiness.Events
{
    public class FooTypeCreateEvent : IFooGooEvent
    {
        public string EventType => FooEventConstants.CreateFooType;

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}