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
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private ApplicationDbContext _context;
        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<NotesController>
        [HttpGet("BoardNotes/board/{boardId}/user/{userId}")]
        public IActionResult GetBoardNotes(int boardId, int userId)
        {
            var notes = _context.Notes.Where(note => note.BoardId == boardId && note.UserId == userId);
            return Ok(notes);
        }

        // GET api/<NotesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var note = _context.Notes.FirstOrDefault(note => note.NotesId == id);
            return Ok(note);
        }

        // POST api/<NotesController>
        [HttpPost("New")]
        public IActionResult Post([FromBody]Notes value)
        {
            _context.Notes.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Notes value)
        {
            var note = _context.Notes.FirstOrDefault(note => note.NotesId == id);
            note.Title = value.Title;
            note.Description = value.Description;
            note.UserId = value.UserId;
            note.BoardId = value.BoardId;
            _context.SaveChanges();
            return Ok(value);

        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = _context.Notes.FirstOrDefault(note => note.NotesId == id);
            _context.Remove(note);
            _context.SaveChanges();
            return Ok();
        }
    }
}
