using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegoCatalog.Data;

namespace LegoCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private PartsCatalogDbContext _context;

        public EventController(PartsCatalogDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            var events = _context.Events.ToList();
            return events;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Event> Item(int id)
        {
            if (id != 0)
            {
                return await _context.Events.Where(e => e.ID == id).FirstOrDefaultAsync();
            }
            else
            {
                return await _context.Events.FirstOrDefaultAsync();
            }
        }

        [HttpPost] //, Route("insert/")]
        public async Task<int> Insert([FromBody] Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return newEvent.ID;
        }

        [HttpPut] //, Route("update/")]
        public async Task<Event> Update([FromBody] Event updatedEvent)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.ID == updatedEvent.ID);
            if (existingEvent == null)
            {
                return null;
            }

            existingEvent.Name = updatedEvent.Name;
            existingEvent.Owner = updatedEvent.Owner;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.EventDate = updatedEvent.EventDate;

            await _context.SaveChangesAsync();

            existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.ID == updatedEvent.ID);
            return existingEvent;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                var eventToDelete = await _context.Events.FirstOrDefaultAsync(e => e.ID == id);
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }        
        }
    }
}