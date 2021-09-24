using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug_Tracker.Controllers
{
    [Route("api/Board")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IBoardRepository _boardRepository;
        public BoardController(ApplicationDbContext context, IBoardRepository boardRepository)
        {
            _context = context;
            _boardRepository = boardRepository;
        }

        // GET: api/Board/id Gets all of a users boards
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllBoards(int id)
        {
            return new ObjectResult(await _boardRepository.GetAllBoards(id));
        }

        // GET: api/Board/InvitedBoards/userId Gets all of the boards a user was invited to
        [HttpGet("InvitedBoards/{userId}")]
        public async Task<IActionResult> GetInvitedBoards(int userId)
        {
            return new ObjectResult(await _boardRepository.GetInvitedBoards(userId));
        }

        // GET: api/Board/CurrentBoard/id/userId Gets the current board a user has clicked on
        [HttpGet("CurrentBoard/{id}/{userId}")]
        public async Task<IActionResult> GetBoard(int id, int userId)
        {
            UserBoard board = await _boardRepository.GetBoard(id, userId);
            if(board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }

        // POST api/Board
        [HttpPost]
        public async Task<IActionResult> AddNewBoard([FromBody] Board value)
        {
            if(ModelState.IsValid)
            {
                bool isValid = await _boardRepository.AddNewBoard(value);
                if (isValid)
                {
                    return Ok(value);
                }
            }
            return BadRequest();

        }

        // POST api/board/addUserToBoard/userId
        [HttpPost("addUserToBoard/{userId}")]
        public async Task <IActionResult>AddBoardToUserBoard(int userId, [FromBody] Board value)
        {
            bool isValid = await _boardRepository.AddBoardToUserBoard(userId, value);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        // POST api/board/acceptedBoardInvitation/userId
        [HttpPost("acceptBoardInvitation/{userId}")]
        public async  Task<IActionResult>AcceptBoardInvite([FromBody] Board value, int userId)
        {
           bool isValid = await _boardRepository.AcceptBoardInvite(value, userId);
           if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/Board/removeBoard/boardId/user/userId
        [HttpDelete("removeBoard/{boardId}/User/{userId}")]
        public async Task<IActionResult>RemoveBoardRelationship(int boardId, int userId)
        {
            bool isValid = await _boardRepository.RemoveBoardRelationship(boardId, userId);
            if(isValid)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
