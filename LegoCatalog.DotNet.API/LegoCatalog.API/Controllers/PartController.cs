using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegoCatalog.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Extensions;
using System.IO;
using LegoCatalog.Service;
using LegoCatalog.DTO;

namespace LegoCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private PartsCatalogDbContext _context;
        private PartService _partService;

        public PartController(PartsCatalogDbContext context, PartService partService)
        {
            _context = context;
            _partService = partService;
        }

        [HttpGet]
        [Route("partId/{partId}")]
        public async Task<Part> GetByPartId(int partId = 0)
        {
            var partQuery = (IQueryable<Part>)_context.Parts.Include(p => p.Category).Include(p => p.ItemType);
            try
            {
                if (partId >= 0)
                {
                    partQuery = partQuery.Where(p => p.PartId == partId);
                    var part = await partQuery.FirstOrDefaultAsync();
                    part.IconLink = $"http://localhost:5000/image/{part.IconLink}";

                    return await partQuery.FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        [HttpGet]
        [Route("{itemId?}")]
        public async Task<Part> Get(string itemId = "")
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return new Part();
            }

            return await _context.Parts.Include(p => p.Category).Include(p => p.ItemType).FirstOrDefaultAsync(p => p.ItemId == itemId);
        }

        [HttpPost]
        [Route("search")]
        public async Task<List<PartDTO>> Search(PartSearchCriteria searchCriteria = null)
        {
            List<PartDTO> parts = await _partService.Search(searchCriteria);

            return (parts == null) ? new List<PartDTO>() : parts;
        }

        [HttpGet]
        [Route("addRemoveQuantity")]
        public async Task<int> UpdateQuantity(int partId, int newQuantity, string colorId = "")
        {
            try
            {
                return await _partService.UpdateQuantity(partId, newQuantity, colorId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return 0;
            }
        }
    }
}
