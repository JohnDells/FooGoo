using System;

namespace FooGooEf
{
    public class FooEntity
    {
        public Guid FooId { get; set; }

        public Guid FooTypeId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}