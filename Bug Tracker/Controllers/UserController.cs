using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
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
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllUsers()
        {
            return new ObjectResult(await _userRepository.GetAllUsers());
        }
        // GET: api/<UserController>
        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetBoardUsers(int boardId)
        {
            return new ObjectResult(await _userRepository.GetBoardUsers(boardId));
        }

        [HttpGet("GetUserRole/Board/{boardId}/User/{userId}")]
        public async Task<IActionResult> GetUserRole(int boardId, int userId)
        {
            return new ObjectResult(await _userRepository.GetUserRole(boardId, userId));
        }



        // GET api/<UserController>/5
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] User value)
        {
            return new ObjectResult(await _userRepository.LoginUser(value));
        }
        // POST api/User>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User value)
        {
            bool isValid = await _userRepository.RegisterUser(value);
            if (isValid)
            {
                return Ok();
            }
            return StatusCode(409);
        }

        [HttpPost("InvitingUserToBoard/{userId}")]
        public async Task<IActionResult> AddUserToUserBoard(int userId, [FromBody] Board value)
        {
            bool isValid = await _userRepository.AddUserToUserBoard(userId, value);
            if(isValid)
            {
                return Ok();
            }
            return StatusCode(409);
        }

        [HttpPost("EditRole/{roleId}")]
        public async Task<IActionResult> EditUserRole([FromBody] UserBoard value, int roleId)
        {
            bool isValid = await _userRepository.EditUserRole(value, roleId);
            if (isValid)
            {
                return Ok();
            }
            return BadRequest();
            
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{userId}/Board/{boardId}")]
        public async Task<IActionResult> RemoveUserFromBoard(int userId, int boardId)
        {
            bool isValid = await _userRepository.RemoveUserFromBoard(userId, boardId);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
