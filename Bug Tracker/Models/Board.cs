﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Models
{
    public class Board
    {
        public int BoardId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserBoard> Users { get; set; }
    }
}
