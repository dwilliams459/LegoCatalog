using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegoCatalog.Data
{
    [Table("PartColor")]
    public class PartColor
    {
        [Key]
        public int PartColorId { get; set; }

        public string ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Part Part { get; set; }
        
        public string Color { get; set; }
        public int CodeName { get; set; }
    }
}