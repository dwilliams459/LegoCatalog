using System;
using Microsoft.EntityFrameworkCore;

namespace LegoCatalog.Data 
{
    public class PartsCatalogDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartColor> PartColors { get; set; }
        public DbSet<InventoryPart> InventoryParts { get; set; }

        public DbSet<Event> Events { get; set; }

        public PartsCatalogDbContext(DbContextOptions<PartsCatalogDbContext> options) : base(options)
        {
            
        }
    }
}