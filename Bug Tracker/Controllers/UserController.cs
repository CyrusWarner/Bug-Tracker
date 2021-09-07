﻿using Bug_Tracker.Data;
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
            var users = _context.UserBoard.Include(u => u.User).Include(r => r.Roles).Where(ub => ub.BoardId == boardId);
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
            if (user == null)
            {
                return StatusCode(404);
            }
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
            var users = _context.UserBoard.Where(ub => ub.BoardId == value.BoardId && ub.UserId == userId);
            if (users.Count() == 0)
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
            return StatusCode(409);
        }

        [HttpPost("EditRole/{roleId}")]
        public IActionResult EditRole([FromBody] UserBoard value, int roleId)
        {
            var oldBoardUserRelationship = _context.UserBoard.Where(ub => ub.UserId == value.UserId && ub.BoardId == value.BoardId).SingleOrDefault();
            _context.UserBoard.Remove(oldBoardUserRelationship);
            UserBoard updatedUserBoardRole = new UserBoard()
            {
                UserId = value.UserId,
                BoardId = value.BoardId,
                RolesId = roleId,
                InviteAccepted = true
            };
            _context.UserBoard.Add(updatedUserBoardRole);
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
