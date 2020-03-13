using LegoCatalog.Data;
using LegoCatalog.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoCatalog.Service
{
    public class PartService
    {
        private PartsCatalogDbContext _context;

        public PartService(PartsCatalogDbContext context)
        {
            _context = context;
        }

        private string BuildFilter(PartSearchCriteria searchCriteria)
        {
            var and = string.Empty;
            var filterBuilder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(searchCriteria.ItemId))
            {
                filterBuilder.AppendLine($"{and} {1} != null && ItemId.ToLower().Contains(\"{searchCriteria.ItemId.ToLower()}\") = true");
                and = " && ";
            }
            if (!string.IsNullOrWhiteSpace(searchCriteria.ItemName))
            {
                filterBuilder.AppendLine($"{and} {1} != null && ItemId.ToLower().Contains(\"{searchCriteria.ItemName.ToLower()}\") = true");
                and = " && ";
            }

            return filterBuilder.ToString();
        }

        private async Task<List<Part>> FindParts(PartSearchCriteria searchCriteria)
        {
            //var filter = BuildFilter(searchCriteria);
            var partQuery = (_context.Parts.Include(p => p.Category).Include(p => p.ItemType)).Where(p => true);

            // partQuery = partQuery.Where("asdf");
            // partQuery = partQuery.Where(p =>
            //     (string.IsNullOrWhiteSpace(searchCriteria.ItemName) ? true : p.ItemName.ToLower().Contains(searchCriteria.ItemName)) && 
            //     (string.IsNullOrWhiteSpace(searchCriteria.ItemId) ? true : p.ItemName.ToLower().Contains(searchCriteria.ItemId)) 
            // );

            if (searchCriteria.PartId != null && searchCriteria.PartId > 0)
            {
                partQuery = partQuery.Where(p => p.PartId.Equals(searchCriteria.PartId));
            }
            if (searchCriteria.ItemId != null && searchCriteria.ItemId.Length > 0)
            {
                partQuery = partQuery.Where(p => p.ItemId.Equals(searchCriteria.ItemId));
            }

            if (searchCriteria.ItemName != null && searchCriteria.ItemName.Length > 0)
            {
                partQuery = partQuery.Where(p => p.ItemName.Contains(searchCriteria.ItemName));
            }
            if (searchCriteria.CategoryName != null && searchCriteria.CategoryName.Length > 0)
                partQuery = partQuery.Where(p => p.Category.Name.Contains(searchCriteria.CategoryName));

            //partQuery = partQuery.Where(p => !string.IsNullOrWhiteSpace(p.IconLink) && p.IconLink != "error");

            if (searchCriteria.Page > 0)
            {
                partQuery = partQuery.Skip(searchCriteria.Page * searchCriteria.PageSize);
            }
            var parts = await partQuery.Take(searchCriteria.PageSize).ToListAsync();

            foreach (var p in parts)
            {
                p.IconLink = $"http://localhost:5000/image/{p.IconLink}";
            }

            return parts;
        }

        public async Task<List<PartDTO>> Search(PartSearchCriteria searchCriteria)
        {
            var partList = await this.FindParts(searchCriteria);
            var partListDTO = new List<PartDTO>();

            foreach (var p in partList)
            {
                var partDto = new PartDTO
                {
                    PartId = p.PartId,
                    ItemId = p.ItemId,
                    ItemName = p.ItemName,
                    CategoryId = p.CategoryId,
                    IconLink = p.IconLink,
                    ImageLink = p.ImageLink,
                    ItemDimensionX = p.ItemDimensionX,
                    ItemDimensionY = p.ItemDimensionY,
                    ItemDimensionZ = p.ItemDimensionZ,
                    ItemTypeId = p.ItemTypeId,
                    ItemTypeName = p.ItemType.ItemTypeName,
                    CategoryName = p.Category.Name
                };

                var i = _context.PartColors.FirstOrDefault();
                partDto.ColorCount = await _context.PartColors.CountAsync(pc => pc.ItemId == p.ItemId);
                partListDTO.Add(partDto);
            }

            return partListDTO;
        }
    }
}