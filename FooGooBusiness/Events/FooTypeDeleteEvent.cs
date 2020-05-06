using System;

namespace FooGooBusiness.Events
{
    public class FooTypeDeleteEvent : IFooGooEvent
    {
        public string EventType => FooEventConstants.DeleteFooType;

        public Guid Id { get; set; }
    }
}