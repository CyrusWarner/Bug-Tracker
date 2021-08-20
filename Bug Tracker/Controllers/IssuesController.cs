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
    public class IssuesController : ControllerBase
    {
        private ApplicationDbContext _context;
        public IssuesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<IssuesController>
        [HttpGet("BoardIssues/{id}")]
        public IActionResult GetBoardIssues(int id)
        {
            var issues = _context.Issues.Where(issue => issue.BoardId == id);
            return StatusCode(200, issues);
        }

        // GET api/<IssuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var currentIssue = _context.Issues.FirstOrDefault(issue => issue.IssuesId == id);
            return Ok(currentIssue);
        }

        // POST api/<IssuesController>
        [HttpPost]
        public IActionResult Post([FromBody]Issues value)
        {
            _context.Issues.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT api/<IssuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Issues value)
        {
            var issue = _context.Issues.FirstOrDefault(issue => issue.IssuesId == id);
            issue.Title = value.Title;
            issue.Description = value.Description;
            issue.isCompleted = value.isCompleted;
            issue.BoardId = value.BoardId;
            issue.UserId = value.UserId;
            _context.SaveChanges();
            return Ok(value);
        }

        // DELETE api/<IssuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var issue = _context.Issues.FirstOrDefault(issue => issue.IssuesId == id);
            _context.Remove(issue);
            _context.SaveChanges();
            return Ok();
        }
    }
}
