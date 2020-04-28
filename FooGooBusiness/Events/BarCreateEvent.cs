using System;

namespace FooGooBusiness.Events
{
    public class BarCreateEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.CreateBar;

        public Guid Id { get; set; }

        public Guid FooId { get; set; }

        public string Name { get; set; }
    }
}