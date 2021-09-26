using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
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
        private readonly INotesRepository _notesRepository;
        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }
        // GET: api/<NotesController>
        [HttpGet("BoardNotes/board/{boardId}/user/{userId}")]
        public async Task <IActionResult> GetBoardNotes(int boardId, int userId)
        {
            return new ObjectResult(await _notesRepository.GetBoardNotes(boardId, userId));
        }

        // GET api/<NotesController>/5
        [HttpGet("{noteId}")]
        public async Task<IActionResult> GetNote(int noteId)
        {
            return new ObjectResult(await _notesRepository.GetNote(noteId));
        }

        // POST api/<NotesController>
        [HttpPost("New")]
        public async Task<IActionResult> AddNewNote([FromBody]Notes value)
        {
            bool isValid = await _notesRepository.AddNewNote(value);
            if (isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<NotesController>/5
        [HttpPut("{noteId}")]
        public async Task<IActionResult> UpdateNote(int noteId, [FromBody]Notes value)
        {
            bool isValid = await _notesRepository.UpdateNote(noteId, value);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{noteId}")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            bool isValid = await _notesRepository.DeleteNote(noteId);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
