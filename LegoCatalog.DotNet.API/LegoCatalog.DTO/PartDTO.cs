using System;
using System.Collections.Generic;
using LegoCatalog.Data;

namespace LegoCatalog.DTO
{
    public class PartDTO 
    {
        public int PartId { get; set; }

        public string ItemId { get; set; }
        public string ItemName { get; set; }
        
        public string ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        public decimal? ItemWeight { get; set; }
        public decimal? ItemDimensionX { get; set; }
        public decimal? ItemDimensionY { get; set; }
        public decimal? ItemDimensionZ { get; set; }
        public string ImageLink { get; set; }
        public string IconLink { get; set; }
        public int ColorCount { get; set; }

        public List<PartColorDTO> PartColors { get; set; } 

        public int Quantity { get; set; }
    }
}