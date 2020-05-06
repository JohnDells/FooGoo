using System;

namespace FooGooDapper
{
    public class FooGooEventRec
    {
        public long SequenceId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public Guid? CorrelationId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}