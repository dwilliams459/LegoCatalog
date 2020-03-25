using Microsoft.Extensions.Configuration;
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
        private IConfiguration _configuration;

        public PartService(PartsCatalogDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Part> GetPartById(int partId = 0)
        {
            var partQuery = (IQueryable<Part>)_context.Parts.Include(p => p.Category).Include(p => p.ItemType);
            try
            {
                if (partId >= 0)
                {
                    string imageBaseUrl = _configuration["IconBaseUrl"];

                    partQuery = partQuery.Where(p => p.PartId == partId);
                    var part = await partQuery.FirstOrDefaultAsync();
                    part.IconLink = $"{imageBaseUrl}/{part.IconLinkJpeg}";

                    return await partQuery.FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
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
            var partQuery = (_context.Parts
                    .Include(p => p.Category)
                    .Include(p => p.ItemType)
                    .Include(p => p.PartColors))
                    .Where(p => true);

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
            if (searchCriteria.ColorOnly)
            {
                partQuery = partQuery.Where(p => p.PartColors != null && p.PartColors.Count > 0);
            }

            //partQuery = partQuery.Where(p => !string.IsNullOrWhiteSpace(p.IconLink) && p.IconLink != "error");

            if (searchCriteria.Page > 0)
            {
                partQuery = partQuery.Skip(searchCriteria.Page * searchCriteria.PageSize);
            }

            List<Part> parts = new List<Part>();
            try
            {
                parts = await partQuery.Take(searchCriteria.PageSize).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Search Error (service): " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            string imageBaseUrl = _configuration["IconBaseUrl"];
            foreach (var p in parts)
            {
                p.ColorQuantity = p.PartColors.Select(pc => pc.Color).Distinct().Count();
                p.IconLink = $"{imageBaseUrl}/{p.IconLinkJpeg}";  // $"http://localhost:5000/image/{p.IconLink}";               
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
                    CategoryName = p.Category.Name,
                    Quantity = p.Quantity,
                    ColorCount = p.ColorQuantity
                };

                partDto.PartColors = await PartColors(p.ItemId);

                partListDTO.Add(partDto);
            }

            return partListDTO;
        }

        public async Task<int> UpdateQuantity(int partId, int newValue, string colorId = "")
        {
            var part = await _context.Parts.FirstOrDefaultAsync(p => p.PartId == partId);
            part.Quantity = newValue;
            await _context.SaveChangesAsync();

            return part.Quantity;
        }

        public async Task<List<PartColorDTO>> PartColors(string itemId)
        {
            var colorQuery = from partColor in _context.PartColors
                             join color in _context.Colors on partColor.Color equals color.ColorName
                             where partColor.ItemId == itemId
                             orderby partColor.CodeName
                             select new PartColorDTO { ItemId = itemId, RGB = color.RGB, Type = color.Type, Color = color.ColorName };

            var colors = await colorQuery.Distinct().ToListAsync();

            return colors;
        }

        public async Task<List<string>> Categories()
        {
            List<string> categories = await _context.Parts.Include(p => p.Category)
                                            .Select(p => p.Category.Name)
                                            .Distinct()
                                            .ToListAsync();
            categories.Sort();
            return categories;
        }
    }
}