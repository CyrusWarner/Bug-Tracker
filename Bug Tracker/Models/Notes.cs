using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Models
{
    public class Notes
    {
        public int NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Board")]
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }

}
