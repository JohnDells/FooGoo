using System;

namespace FooGooBusiness.Events
{
    public class BarDeleteEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.DeleteBar;

        public Guid Id { get; set; }
    }
}