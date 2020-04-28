using System;

namespace FooGooBusiness.Events
{
    public class FooTypeUpdateNameEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.UpdateFooTypeName;

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}