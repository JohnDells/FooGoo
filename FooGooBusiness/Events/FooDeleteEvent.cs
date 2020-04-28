using System;

namespace FooGooBusiness.Events
{
    public class FooDeleteEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.DeleteFoo;

        public Guid Id { get; set; }
    }
}