using Bug_Tracker.Data;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private ApplicationDbContext _context;
        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<BoardController>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var boards = _context.UserBoard.Include(ub => ub.Board).Where(b => b.UserId == id);
            return Ok(boards);
        }

        // GET api/<BoardController>/5
        //[HttpGet("{id}")]
        //public IActionResult GetusersBoard(int id)
        //{

        //}
        [HttpGet("CurrentBoard/{id}")]
        public IActionResult GetBoard(int id)
        {
            //QUERY ALL BOARDS OF A USER FROM JUNCTION TABLE HERE
            var board = _context.Boards.Where(board => board.BoardId == id);
            return Ok(board);

        }

        // POST api/<BoardController>
        [HttpPost]
        public IActionResult Post([FromBody] Board value)
        {
            _context.Boards.Add(value);
            _context.SaveChanges();
            return StatusCode(200, value);
        }

        [HttpPost("addUserToBoard/{userId}")]
        public IActionResult AddBoardToUserBoard(int userId, [FromBody] Board value)
        {
            var newUserBoard = new UserBoard()
            {
                UserId = userId,
                BoardId = value.BoardId,
                RolesId = 3,
                InviteAccepted = true,
            };
            _context.UserBoard.Add(newUserBoard);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("acceptBoardInvitation/{userId}")]
        public IActionResult AcceptBoardInvite([FromBody] Board value, int userId)
        {
            var oldUserBoardRelationship = _context.UserBoard.Where(ub => ub.BoardId == value.BoardId && ub.UserId == userId).SingleOrDefault();
            _context.UserBoard.Remove(oldUserBoardRelationship);
            UserBoard newUserBoard = new UserBoard()
            {
                UserId = userId,
                BoardId = value.BoardId,
                RolesId = 2,
                InviteAccepted = true,
            };
            _context.UserBoard.Add(newUserBoard);
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<BoardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
