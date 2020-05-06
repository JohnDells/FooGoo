using System;

namespace FooGooBusiness.Events
{
    public class BarDeleteEvent : IFooGooEvent
    {
        public string EventType => FooEventConstants.DeleteBar;

        public Guid Id { get; set; }
    }
}