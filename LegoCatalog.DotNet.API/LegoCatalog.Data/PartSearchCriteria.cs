using System;

namespace LegoCatalog.Data
{
    public class PartSearchCriteria : SearchCriteria
    {
        public int? PartId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
    }
}