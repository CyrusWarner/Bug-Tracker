using Bug_Tracker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable> GetAllUsers();
        Task<IEnumerable> GetBoardUsers(int boardId);
        Task<IEnumerable> GetUserRole(int boardId, int userId);
        Task<User> LoginUser(User user);
        Task<bool> RegisterUser(User user);
        Task<bool> AddUserToUserBoard(int userId, Board board);
        Task<bool> EditUserRole(UserBoard userBoard, int roleId);
        Task<bool> RemoveUserFromBoard(int userId, int boardId);
    }
}
