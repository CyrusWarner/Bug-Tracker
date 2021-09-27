using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable> GetBoardUsers(int boardId)
        {
            return await _context.UserBoard.Include(u => u.User).Include(r => r.Roles).Where(ub => ub.BoardId == boardId).ToListAsync();
        }

        public async Task<IEnumerable> GetUserRole(int boardId, int userId)
        {
            return await _context.UserBoard.Include(ur => ur.Roles).Where(b => b.BoardId == boardId && b.UserId == userId).ToListAsync();
        }

        public async Task<User> LoginUser(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
        }

        public async Task<bool> RegisterUser(User user)
        {
            var userExists = _context.Users.Any(user => user.Email == user.Email);
            if(!userExists)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddUserToUserBoard(int userId, Board board)
        {
            var userExistsOnBoard = _context.UserBoard.Any(ub => ub.BoardId == board.BoardId && ub.UserId == userId);
            if(!userExistsOnBoard)
            {
                var newUserBoard = new UserBoard()
                {
                    UserId = userId,
                    BoardId = board.BoardId,
                    RolesId = 2,
                    InviteAccepted = false,
                };
                _context.UserBoard.Add(newUserBoard);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditUserRole(UserBoard userBoard, int roleId)
        {
            var oldBoardUserRelationship = _context.UserBoard.Where(ub => ub.UserId == userBoard.UserId && ub.BoardId == userBoard.BoardId).SingleOrDefault();
            if(oldBoardUserRelationship != null)
            {
                _context.UserBoard.Remove(oldBoardUserRelationship);
                UserBoard updatedUserBoardRole = new UserBoard()
                {
                    UserId = userBoard.UserId,
                    BoardId = userBoard.BoardId,
                    RolesId = roleId,
                    InviteAccepted = true
                };
                _context.UserBoard.Add(updatedUserBoardRole);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveUserFromBoard(int userId, int boardId)
        {
            var userBoardRelationship = _context.UserBoard.Where(ur => ur.UserId == userId && ur.BoardId == boardId);
            foreach (UserBoard userRelationship in userBoardRelationship)
            {
                _context.Remove(userRelationship);

            }
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
