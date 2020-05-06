using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FooGooEf
{
    [Table("FooGooSnapshot")]
    public class FooGooSnapshotEntity
    {
        [Key]
        public Guid Id { get; set; }

        public long CurrentSequenceId { get; set; }

        public SnapshotType SnapshotType { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}