using Bug_Tracker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Interfaces
{
    public interface IIssuesRepository
    {
        Task<IEnumerable> GetBoardIssues(int id);
        Task<IEnumerable> GetIssue(int id);
        Task<bool> AddNewIssue(Issues issue);
        Task<bool> UpdateIssue(int id, Issues issue);
        Task<bool> DeleteIssue(int id);


    }
}
