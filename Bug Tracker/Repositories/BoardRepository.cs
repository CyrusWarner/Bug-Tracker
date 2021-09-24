using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _context;
        public BoardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool>BoardExists(int boardId)
        {
            return await _context.Boards.FirstOrDefaultAsync(b => b.BoardId == boardId) != null;
        }

        public async Task<IEnumerable>GetAllBoards(int id)
        {
            return await _context.UserBoard.Include(ub => ub.Board).Where(b => b.UserId == id && b.InviteAccepted).ToListAsync();
        }

        public async Task<IEnumerable>GetInvitedBoards(int userId)
        {
            return await _context.UserBoard.Include(ub => ub.Board).Where(b => b.UserId == userId && b.InviteAccepted == false).ToListAsync();
        }

        public async Task<UserBoard>GetBoard(int id, int userId)
        {
            return await _context.UserBoard.Include(b => b.Board).FirstOrDefaultAsync(ub => ub.BoardId == id && ub.UserId == userId);
        }

        public async Task<bool>AddNewBoard(Board board)
        {
            if(board != null)
            {
                _context.Boards.Add(board);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool>AddBoardToUserBoard(int userId, Board board)
        {
            bool boardExists = await BoardExists(board.BoardId);
            if(!boardExists)
            {
                var newUserBoard = new UserBoard()
                {
                    UserId = userId,
                    BoardId = board.BoardId,
                    RolesId = 3,
                    InviteAccepted = true,
                };
                _context.UserBoard.Add(newUserBoard);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool>AcceptBoardInvite(Board board, int userId)
        {
            bool boardExists = await BoardExists(board.BoardId);
            if (!boardExists)
            {
                var oldUserBoardRelationship = _context.UserBoard.Where(ub => ub.BoardId == board.BoardId && ub.UserId == userId).SingleOrDefault();
                _context.UserBoard.Remove(oldUserBoardRelationship);
                UserBoard newUserBoard = new UserBoard()
                {
                    UserId = userId,
                    BoardId = board.BoardId,
                    RolesId = 2,
                    InviteAccepted = true,
                };
                _context.UserBoard.Add(newUserBoard);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveBoardRelationship(int boardId, int userId)
        {
            var userBoardRelationship = _context.UserBoard.Where(ub => ub.BoardId == boardId && ub.UserId == userId).SingleOrDefault();
            if(userBoardRelationship != null)
            {
                _context.UserBoard.Remove(userBoardRelationship);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
