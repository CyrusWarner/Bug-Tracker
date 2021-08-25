using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Models
{
    public class Events
    {
        public int EventsId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Assignee { get; set; }

        [ForeignKey("Board")]
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
