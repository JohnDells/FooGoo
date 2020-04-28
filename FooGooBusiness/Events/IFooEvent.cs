using System;

namespace FooGooBusiness.Events
{
    public interface IFooEvent
    {
        public string Type { get; }

        public Guid Id { get; }
    }
}