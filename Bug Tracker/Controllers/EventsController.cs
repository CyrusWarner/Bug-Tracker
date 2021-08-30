using Bug_Tracker.Data;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug_Tracker.Controllers
{
    [Route("api/Events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET api/Events/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var events = _context.Events.Where(eve => eve.BoardId == id);
            return Ok(events);
        }

        // POST api/<EventsController>
        [HttpPost]
        public IActionResult Post([FromBody] Events value)
        {
            _context.Events.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var eventToDelete = _context.Events.FirstOrDefault(e => e.EventsId == id);
            _context.Remove(eventToDelete);
            _context.SaveChanges();
            return Ok();
        }
    }
}
