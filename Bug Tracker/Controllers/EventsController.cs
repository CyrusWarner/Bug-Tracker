using Bug_Tracker.Data;
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
        private readonly IWebHostEnvironment _hostEnvir;

        private ApplicationDbContext _context;
        public EventsController(ApplicationDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _hostEnvir = hostEnvir;
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

        [HttpPost("uploadfile/csv/{boardId}")]
       public IActionResult PostCsv([FromForm] IFormFile inputFile, int boardId)
        {
            string uploads = Path.Combine(_hostEnvir.ContentRootPath, "UploadedFiles");
            var uploadTime = DateTime.Now.Ticks.ToString();
            string filePath = string.Empty;
            //Create directory if it doesn't exist 
            Directory.CreateDirectory(uploads);
           
                if (inputFile.Length > 0)
                {
                     filePath = Path.Combine(uploads, $"NewFile_{uploadTime}_{inputFile.FileName}");
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                     inputFile.CopyTo(fileStream);
                    }
                }
            
            using (var streamReader = new StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] line = streamReader.ReadLine().Split(',');
                    bool isValid = ValidateLine(line);

                    if(!isValid)
                    {
                        return StatusCode(400);
                    }

                    _context.Events.Add(new Events()
                    {
                        Title = line[0],
                        Date = line[1],
                        BoardId = boardId,
                        Assignee = line[2]
                    });
                    _context.SaveChanges();

                }

            }
            return Ok();
        }

        private bool ValidateLine(string[] line)
        {
            var isValid = true;

            if(line == null)
            {
                return false;
            }

            if(line[0] == "")
            {
                return false;
            }

            if(line[1] == "")
            {
                return false;
            }
            try
            {
                DateTime.Parse(line[1]);
            }
            catch
            {
                return false;
            }

            if(DateTime.Parse(line[1]) == DateTime.MinValue)
            {
                return false;
            }

            return isValid;
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
        [HttpPatch]
        public IActionResult ChangeEventDate([FromBody] Events value)
        {
            var eventToEdit = _context.Events.Where(e => e.EventsId == value.EventsId).FirstOrDefault();
            eventToEdit.Date = value.Date;
            _context.SaveChanges();
            return Ok();
        }
    }
}
