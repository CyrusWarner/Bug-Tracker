using Bug_Tracker.Models;
using System.Collections;
using System.Threading.Tasks;

namespace Bug_Tracker.Interfaces
{
    public interface IBoardRepository
    {
        Task<bool>BoardExists(int boardId);
        Task<IEnumerable>GetAllBoards(int id);
        Task<IEnumerable>GetInvitedBoards(int userId);
        Task<UserBoard>GetBoard(int id, int userId);
        Task<Board>AddNewBoard(Board board);
        Task<bool>AddBoardToUserBoard(int userId, Board board);
        Task<bool>AcceptBoardInvite(Board board, int userId);
        Task<bool>RemoveBoardRelationship(int boardId, int userId);

    }
}
