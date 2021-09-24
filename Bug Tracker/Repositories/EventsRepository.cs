using Bug_Tracker.Data;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvir;

        public EventsRepository(ApplicationDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _hostEnvir = hostEnvir;
        }

        public async Task<bool> EventExists(int eventId)
        {
            return await _context.Events.FirstOrDefaultAsync(eve => eve.EventsId == eventId) != null;
        }

        public async Task<IEnumerable> GetBoardEvents(int id)
        {
            return await _context.Events.Where(eve => eve.BoardId == id).ToListAsync();
        }

        public async Task<bool> AddNewEvent(Events eventValue)
        {
            bool eventExists = await EventExists(eventValue.EventsId);
            if(!eventExists)
            {
                _context.Events.Add(eventValue);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool>DeleteEvent(int id)
        {
            bool eventExists = await EventExists(id);
            if(eventExists)
            {
                var eventToDelete = _context.Events.FirstOrDefault(e => e.EventsId == id);
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeEventDate(Events eventValue)
        {
            bool eventExists = await EventExists(eventValue.EventsId);
            if (eventExists)
            {
                var eventToEdit = _context.Events.Where(e => e.EventsId == eventValue.EventsId).FirstOrDefault();
                eventToEdit.Date = eventValue.Date;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> PostCsv(IFormFile inputFile, int boardId)
        {
            string uploads = Path.Combine(_hostEnvir.ContentRootPath, "UploadedFiles");
            var uploadTime = DateTime.Now.Ticks.ToString();
            string filePath = string.Empty;
            //Create directory if it doesn't exist 
            Directory.CreateDirectory(uploads);

            if (inputFile.Length > 0)
            {
                filePath = Path.Combine(uploads, $"NewFile_{uploadTime}_{inputFile.FileName}");
                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    inputFile.CopyTo(fileStream);
                }
            }

            using (var streamReader = new StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] line = streamReader.ReadLine().Split(',');
                    bool isValid = ValidateLine(line);

                    if (!isValid)
                    {
                        return false;
                    }

                    _context.Events.Add(new Events()
                    {
                        Title = line[0],
                        Date = line[1],
                        BoardId = boardId,
                        Assignee = line[2]
                    });
                    await _context.SaveChangesAsync();

                }

            }
            return true;
        }

        private static bool ValidateLine(string[] line)
        {
            var isValid = true;

            if (line == null)
            {
                return false;
            }

            if (line[0] == "")
            {
                return false;
            }

            if (line[1] == "")
            {
                return false;
            }
            try
            {
                DateTime.Parse(line[1]);
            }
            catch
            {
                return false;
            }

            if (DateTime.Parse(line[1]) == DateTime.MinValue)
            {
                return false;
            }

            return isValid;
        }

    }
}
