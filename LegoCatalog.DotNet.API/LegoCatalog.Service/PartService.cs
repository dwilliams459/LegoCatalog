﻿using Microsoft.Extensions.Configuration;
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
                var part = new Part();

                if (partId == 0)
                {
                    partQuery = partQuery.OrderBy(p => new Guid());
                    part = await partQuery.FirstOrDefaultAsync();
                }
                else
                {
                    partQuery = partQuery.Where(p => p.PartId == partId);
                    part = await partQuery.FirstOrDefaultAsync();
                }

                string imageBaseUrl = _configuration["IconBaseUrl"];
                part.IconLink = $"{imageBaseUrl}/{part.IconLinkJpeg}";
                return await partQuery.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
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

            string searchItemName = (searchCriteria.ItemName == null) ? string.Empty : searchCriteria.ItemName.ToLower().Trim();

            // General Search
            if (searchCriteria.ItemName != null && searchCriteria.ItemName.Length > 0)
            {
                // 'Cat:' prefix for category
                if (searchItemName.StartsWith("c:"))
                    partQuery = partQuery.Where(p => p.Category.Name.StartsWith(searchCriteria.ItemName.Replace("cat:", "")));
                else if (searchItemName.StartsWith("p:"))
                    partQuery = partQuery.Where(p => p.PartId.ToString().StartsWith(searchCriteria.ItemName.Replace("part:", "")));
                else if (searchItemName.StartsWith("i:"))
                    partQuery = partQuery.Where(p => p.ItemId.StartsWith(searchCriteria.ItemName.Replace("item:", "")));
                else
                {
                    partQuery = partQuery.Where(p => p.ItemName.Contains(searchCriteria.ItemName)
                                                    || p.PartId.ToString().StartsWith(searchCriteria.ItemName)
                                                    || p.Category.Name.Contains(searchCriteria.ItemName)
                                                    || p.ItemId.StartsWith(searchCriteria.ItemName));
                }
            }

            if (searchCriteria.PartId != null && searchCriteria.PartId > 0)
            {
                partQuery = partQuery.Where(p => p.PartId.Equals(searchCriteria.PartId));
            }
            if (searchCriteria.ItemId != null && searchCriteria.ItemId.Length > 0)
            {
                partQuery = partQuery.Where(p => p.ItemId.Equals(searchCriteria.ItemId));
            }


            // Category Drop down
            if (searchCriteria.CategoryName != null && searchCriteria.CategoryName.Length > 0)
                partQuery = partQuery.Where(p => p.Category.Name.Contains(searchCriteria.CategoryName));

            // Return results with color grid
            if (searchCriteria.ColorOnly)
            {
                partQuery = partQuery.Where(p => p.PartColors != null && p.PartColors.Count > 0);
            }

            Console.WriteLine($"searchCriteria.SizeX: {searchCriteria.SizeXNum}");
            if (searchCriteria.SizeXNum > 0)
            {
                partQuery = partQuery.Where(p => p.ItemDimensionX == searchCriteria.SizeXNum);
            }
            if (searchCriteria.SizeYNum > 0)
            {
                partQuery = partQuery.Where(p => p.ItemDimensionY == searchCriteria.SizeYNum);
            }
            if (searchCriteria.SizeZNum > 0)
            {
                partQuery = partQuery.Where(p => p.ItemDimensionZ == searchCriteria.SizeZNum);
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

        //private static IQueryable<Part> SearchPrefix(PartSearchCriteria searchCriteria, IQueryable<Part> partQuery, string prefix)
        private static void SearchPrefix(PartSearchCriteria searchCriteria, IQueryable<Part> partQuery, string prefix)
        {
            var searchItemValue = searchCriteria.ItemName.Replace(prefix, "");
            partQuery = partQuery.Where(p => p.Category.Name.StartsWith(searchItemValue));
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