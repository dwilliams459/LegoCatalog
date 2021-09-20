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
        public string SizeX { get; set; }
        public string SizeY { get; set; }
        public string SizeZ { get; set; }

        public int? SizeXNum { get => (int.TryParse(this.SizeX, out int outSize)) ? outSize : 0; }
        public int? SizeYNum { get => (int.TryParse(this.SizeX, out int outSize)) ? outSize : 0; }
        public int? SizeZNum { get => (int.TryParse(this.SizeX, out int outSize)) ? outSize : 0; }
    }
}