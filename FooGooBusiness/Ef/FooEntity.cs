using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FooGooBusiness.Ef
{
    [Table("Foos")]
    public class FooEntity
    {
        [Key]
        public Guid FooId { get; set; }

        public Guid FooTypeId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}