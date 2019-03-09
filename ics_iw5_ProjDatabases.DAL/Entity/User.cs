using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ics_iw5_ProjDatabases.DAL.Entity.Base;

namespace ics_iw5_ProjDatabases.DAL.Entity
{   
    [Table("User")]
    public class User : BaseEntity
    {
        [Column]
        public string Name { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public string Nick { get; set; }
        [Column]
        public DateTime LastActivity { get; set; } // Date and Time of last activity ( add coment/post, add user to team .... )
        [Column]
        public ICollection<Team> Teams { get; set; }
    }
}
