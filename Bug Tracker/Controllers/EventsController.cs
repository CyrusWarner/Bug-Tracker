using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug_Tracker.Controllers
{
    [Route("api/Events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepository _eventsRepository;
        public EventsController(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }
        // GET api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardEvents(int id)
        {
            return new ObjectResult(await _eventsRepository.GetBoardEvents(id));
        }

        // POST api/<EventsController>
        [HttpPost]
        public async Task<IActionResult> AddNewEvent([FromBody] Events eventvalue)
        {
            bool isValid = await _eventsRepository.AddNewEvent(eventvalue);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("uploadfile/csv/{boardId}")]
       public async Task<IActionResult> PostCsv([FromForm] IFormFile inputFile, int boardId)
        {
            bool isValid = await _eventsRepository.PostCsv(inputFile, boardId);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteEvent(int id)
        {
            bool isValid = await _eventsRepository.DeleteEvent(id);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPatch]
        public async Task<IActionResult> ChangeEventDate([FromBody] Events value)
        {
            bool isValid = await _eventsRepository.ChangeEventDate(value);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
