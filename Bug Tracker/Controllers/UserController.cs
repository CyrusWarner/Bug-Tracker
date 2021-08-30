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
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users;
            return Ok(users);
        }
        // GET: api/<UserController>
        [HttpGet("{boardId}")]
        public IActionResult Get(int boardId)
        {
            var users = _context.Users.Where(u => u.Boards.Any(b => b.BoardId == boardId));
            return Ok(users);
        }

        [HttpGet("GetUserRole/Board/{boardId}/User/{userId}")]
        public IActionResult GetUserRole(int boardId, int userId)
        {
            var userRole = _context.UserBoard.Include(ur => ur.Roles).Where(u => u.BoardId == boardId && u.UserId == userId);
            return Ok(userRole);
        }



        // GET api/<UserController>/5
        [HttpPost("Login")]
        public IActionResult GetUser([FromBody] User value)
        {
                var user = _context.Users.FirstOrDefault(user => user.Email == value.Email && user.Password == value.Password);
                return StatusCode(200, user);

        }
        // POST api/User>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            var users = _context.Users.Where(user => user.Email == value.Email);
            if (users.Count() == 0)
            {
                _context.Users.Add(value);
                _context.SaveChanges();
                return StatusCode(200, value);
            }
            return StatusCode(409);

        }

        [HttpPost("InvitingUserToBoard/{userId}")]
        public IActionResult AddBoardToUserBoard(int userId, [FromBody] Board value)
        {
            var newUserBoard = new UserBoard()
            {
                UserId = userId,
                BoardId = value.BoardId,
                RolesId = 2,
            };
            _context.UserBoard.Add(newUserBoard);
            _context.SaveChanges();
            return Ok();
        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UserController>/5
        [HttpDelete("{userId}/Board/{boardId}")]
        public IActionResult Delete(int userId, int boardId)
        {
            var userBoardRelationship = _context.UserBoard.Where(ur => ur.UserId == userId && ur.BoardId == boardId);
            foreach(UserBoard userRelationship in userBoardRelationship)
            {
                _context.Remove(userRelationship);

            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
