using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FooGooBusiness.Ef
{
    [Table("Bars")]
    public class BarEntity
    {
        [Key]
        public Guid BarId { get; set; }

        public Guid FooId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}