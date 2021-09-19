using System;

namespace LegoCatalog.Data
{
    public class PartSearchCriteria : SearchCriteria
    {
        public int? PartId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public bool ColorOnly { get; set; }
        public bool DisplayColors { get; set; }
        public string CategoryName { get; set; }
        public int? SizeX { get; set; }
        public int? SizeY { get; set; }
        public int? SizeZ { get; set; }
    }
}