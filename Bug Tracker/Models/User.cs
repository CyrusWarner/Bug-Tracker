
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bug_Tracker.Models
{
    [DataContract]
    public class User
    {
        //included in Json
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }

        public virtual ICollection<UserBoard> Boards { get; set; }

        //ignored
        [DataMember]
        public string Password { get; set; }
    }
}
