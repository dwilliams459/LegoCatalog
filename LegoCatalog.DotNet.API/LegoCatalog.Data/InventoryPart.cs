using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegoCatalog.Data
{
    public class InventoryPart
    {
        [Key]
        public int InventoryPartId { get; set; }
        public string ItemId { get; set; }

        
        [Column("Color")]
        public string ColorName { get; set; }
        public Color Color { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    } 
}

