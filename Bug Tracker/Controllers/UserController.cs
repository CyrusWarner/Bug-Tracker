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
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users;
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("Login")]
        public IActionResult Get([FromBody]User value)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == value.Email && user.Password == value.Password);
            return StatusCode(200, user);
        }

        // POST api/User>
        [HttpPost]
        public IActionResult Post([FromBody]User value)
        {
            _context.Users.Add(value);
            _context.SaveChanges();
            return StatusCode(200, value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
