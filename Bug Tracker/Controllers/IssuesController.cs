using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using Bug_Tracker.Repositories;
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
        private readonly IIssuesRepository _issuesRepository;
        public IssuesController(IIssuesRepository issuesRepository)
        {
            _issuesRepository = issuesRepository;
        }
        // GET: api/<IssuesController>
        [HttpGet("BoardIssues/{id}")]
        public async Task<IActionResult> GetBoardIssues(int id)
        {
            return new ObjectResult(await _issuesRepository.GetBoardIssues(id));
        }

        // POST api/<IssuesController>
        [HttpPost]
        public async Task<IActionResult> AddNewIssue([FromBody]Issues value)
        {
            bool isValid = await _issuesRepository.AddNewIssue(value);
            if (isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<IssuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, [FromBody] Issues value)
        {
            bool isValid = await _issuesRepository.UpdateIssue(id, value);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<IssuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            bool isValid = await _issuesRepository.DeleteIssue(id);
            if (isValid)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
