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
    public class IssuesRepository : IIssuesRepository
    {
        private readonly ApplicationDbContext _context;

        public IssuesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IssueExists(int issueId)
        {
            return await _context.Issues.FirstOrDefaultAsync(issue => issue.IssuesId == issueId) != null;
        }
        public async Task<IEnumerable> GetBoardIssues(int id)
        {
            return await _context.Issues.Where(issue => issue.BoardId == id).ToListAsync();
        }
        public async Task<IEnumerable> GetIssue(int id)
        {
            return await _context.Issues.Where(issue => issue.IssuesId == id).ToListAsync();
        }
        public async Task<bool> AddNewIssue(Issues issue)
        {
            bool issueExists = await IssueExists(issue.IssuesId);
            if(!issueExists)
            {
                _context.Issues.Add(issue);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateIssue(int id, Issues issue)
        {
            bool issueExists = await IssueExists(id);
            if(issueExists)
            {
                var issueToEdit = _context.Issues.FirstOrDefault(issue => issue.IssuesId == id);
                issueToEdit.Title = issue.Title;
                issueToEdit.Description = issue.Description;
                issueToEdit.isCompleted = issue.isCompleted;
                issueToEdit.UserId = issue.UserId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteIssue(int id)
        {
            bool issueExists = await IssueExists(id);
            if(issueExists)
            {
                var issue = _context.Issues.FirstOrDefault(issue => issue.IssuesId == id);
                _context.Remove(issue);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
