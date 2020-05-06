using System;

namespace FooGooBusiness
{
    public interface IFooGooEvent
    {
        public string EventType { get; }

        public Guid Id { get; }
    }
}