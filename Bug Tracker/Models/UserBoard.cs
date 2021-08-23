using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Models
{
    public class UserBoard
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
