using Bug_Tracker.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Interfaces
{
    public interface IEventsRepository
    {
        Task<bool> EventExists(int eventId);
        Task<IEnumerable> GetBoardEvents(int id);
        Task<bool> AddNewEvent(Events eventValue);
        Task<bool> DeleteEvent(int id);
        Task<bool> ChangeEventDate(Events eventValue);
        Task<bool> PostCsv(IFormFile inputFile, int boardId);
    }
}
