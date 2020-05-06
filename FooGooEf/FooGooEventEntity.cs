using System;

namespace FooGooEf
{
    public class FooGooEventEntity
    {
        public long SequenceId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public Guid? CorrelationId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}