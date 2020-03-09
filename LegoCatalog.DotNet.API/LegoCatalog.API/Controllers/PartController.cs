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

namespace LegoCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private PartsCatalogDbContext _context;

        public PartController(PartsCatalogDbContext context)
        {
            _context = context;
        }

        // private Uri GetAbsoluteUri()
        // {
        //     var request = _httpContextAccessor.HttpContext.Request;
        //     UriBuilder uriBuilder = new UriBuilder();
        //     uriBuilder.Scheme = request.Scheme;
        //     uriBuilder.Host = request.Host.Host;
        //     uriBuilder.Path = request.Path.ToString();
        //     uriBuilder.Query = request.QueryString.ToString();
        //     return uriBuilder.Uri;
        // }

        // GET: /<controller>/

        private string sourcePath = "c:\\LegoCatalog\\images";

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
            catch (Exception ex)
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
        public async Task<List<Part>> Search(PartSearchCriteria searchCriteria = null)
        {
            var partQuery = (_context.Parts.Include(p => p.Category).Include(p => p.ItemType)).Where(p => true);

            if (searchCriteria.PartId != null && searchCriteria.PartId > 0)
            {
                partQuery = partQuery.Where(p => p.PartId.Equals(searchCriteria.PartId));
            }
            else if (searchCriteria.ItemId != null && searchCriteria.ItemId.Length > 0)
            {
                partQuery = partQuery.Where(p => p.ItemId == searchCriteria.ItemId);
            }
            else
            {
                if (searchCriteria.ItemName != null && searchCriteria.ItemName.Length > 0)
                {
                    partQuery = partQuery.Where(p => p.ItemName.Contains(searchCriteria.ItemName));
                }
                if (searchCriteria.CategoryName != null && searchCriteria.CategoryName.Length > 0)
                    partQuery = partQuery.Where(p => p.Category.Name.Contains(searchCriteria.CategoryName));
            }

            partQuery = partQuery.Where(p => !string.IsNullOrWhiteSpace(p.IconLink) && p.IconLink != "error" );

            if (searchCriteria.Page > 0)
            {
                partQuery = partQuery.Skip(searchCriteria.Page * searchCriteria.PageSize);
            }
            var parts = await partQuery.Take(searchCriteria.PageSize).ToListAsync();

            foreach (var p in parts)
            {
                p.IconLink =  $"http://localhost:5000/image/{p.IconLink}";
            }

            return (parts == null) ? new List<Part>() : parts;
        }
    }
}
