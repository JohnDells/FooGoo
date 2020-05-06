using System;

namespace FooGooBusiness.Events
{
    public class FooDeleteEvent : IFooGooEvent
    {
        public string EventType => FooEventConstants.DeleteFoo;

        public Guid Id { get; set; }
    }
}