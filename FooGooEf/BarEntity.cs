using System;

namespace FooGooEf
{
    public class BarEntity
    {
        public Guid BarId { get; set; }

        public Guid FooId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}