using System;
using System.ComponentModel.DataAnnotations;

namespace LegoCatalog.Data
{
    public class ItemType 
    {
        [Key]
        public string ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
    }
}