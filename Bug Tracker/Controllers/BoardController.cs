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
    [Route("api/Board")]
    [ApiController]
    public class BoardController : ControllerBase
    {
            private  ApplicationDbContext _context;
            public BoardController(ApplicationDbContext context)
            {
                _context = context;
            }
            // GET: api/<BoardController>
            [HttpGet]
        public IActionResult Get()
        {
            var boards = _context.Boards;
            return Ok(boards);
        }

        // GET api/<BoardController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userBoards = _context.Boards.Where(board => board.UserId == id);
            return Ok(userBoards);
        }

        // POST api/<BoardController>
        [HttpPost]
        public IActionResult Post([FromBody]Board value)
        {
            _context.Boards.Add(value);
            _context.SaveChanges();
            return StatusCode(200, value);

        }

        // PUT api/<BoardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BoardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
