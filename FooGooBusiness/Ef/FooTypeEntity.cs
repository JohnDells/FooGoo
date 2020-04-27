using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FooGooBusiness.Ef
{
    [Table("FooTypes")]
    public class FooTypeEntity
    {
        [Key]
        public Guid FooTypeId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}