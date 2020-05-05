using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FooGooEf
{
    [Table("FooGooEvent")]
    public class FooGooEventEntity
    {
        [Key]
        public long SequenceId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public Guid? CorrelationId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}