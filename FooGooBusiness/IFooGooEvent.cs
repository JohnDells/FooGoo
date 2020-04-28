using System;

namespace FooGooBusiness
{
    public interface IFooGooEvent
    {
        public string Type { get; }

        public Guid Id { get; }
    }
}