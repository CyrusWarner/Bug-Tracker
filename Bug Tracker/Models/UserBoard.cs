using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Roles")]
        public int RolesId { get; set; }
        public Roles Roles { get; set; }
    }
}
