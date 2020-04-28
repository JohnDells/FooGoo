using System;

namespace FooGooBusiness.Events
{
    public class FooTypeDeleteEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.DeleteFooType;

        public Guid Id { get; set; }
    }
}