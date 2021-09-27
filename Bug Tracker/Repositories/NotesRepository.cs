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
    public class NotesRepository : INotesRepository
    {
        private readonly ApplicationDbContext _context;
        
        public NotesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> NoteExists(int noteId)
        {
            return await _context.Notes.FirstOrDefaultAsync(note => note.NotesId == noteId) != null;
        }

        public async Task<IEnumerable> GetBoardNotes(int boardId, int userId)
        {
            return await _context.Notes.Where(note => note.BoardId == boardId && note.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable> GetNote(int noteId)
        {
            return await _context.Notes.Where(note => note.NotesId == noteId).ToListAsync();
        }

        public async Task<bool> AddNewNote(Notes note)
        {
              _context.Notes.Add(note);
              await _context.SaveChangesAsync();
              return true;
        }

        public async Task<bool> UpdateNote(int noteId, Notes note)
        {
            bool noteExists = await NoteExists(noteId);
            if(noteExists)
            {
                var noteToEdit = _context.Notes.FirstOrDefault(note => note.NotesId == noteId);
                noteToEdit.Title = note.Title;
                noteToEdit.Description = note.Description;
                noteToEdit.UserId = note.UserId;
                noteToEdit.BoardId = note.BoardId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteNote(int noteId)
        {
            bool noteExists = await NoteExists(noteId);
            if(noteExists)
            {
                var note = _context.Notes.FirstOrDefault(note => note.NotesId == noteId);
                _context.Remove(note);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
