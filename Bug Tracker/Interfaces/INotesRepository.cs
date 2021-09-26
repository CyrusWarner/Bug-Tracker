using Bug_Tracker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Interfaces
{
    public interface INotesRepository
    {
         Task<IEnumerable> GetBoardNotes(int boardId, int userId);
         Task<IEnumerable> GetNote(int noteId);
         Task<bool> AddNewNote(Notes note);
         Task<bool> UpdateNote(int noteId, Notes note);
         Task<bool> DeleteNote(int noteId);
    }
}
