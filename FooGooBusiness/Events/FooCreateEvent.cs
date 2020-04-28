using System;

namespace FooGooBusiness.Events
{
    public class FooCreateEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.CreateFoo;

        public Guid Id { get; set; }

        public Guid FooTypeId { get; set; }

        public string Name { get; set; }
    }
}