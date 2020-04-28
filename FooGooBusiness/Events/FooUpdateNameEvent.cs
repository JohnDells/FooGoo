using System;

namespace FooGooBusiness.Events
{
    public class FooUpdateNameEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.UpdateFooName;

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}