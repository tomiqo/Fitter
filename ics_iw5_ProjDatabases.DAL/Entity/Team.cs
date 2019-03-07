using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ics_iw5_ProjDatabases.DAL.Entity.Base;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Team")]
    public class Team : BaseEntity 
    {
        [Column]
        public int MemberCount { get; set; }
        [Column]
        public string TeamName { get; set; }
        [Column]
        public DateTime DateOfCreation { get; set; } //20.12.2019 , 12-20-2019 .. 
        [Column]
        [ForeignKey("UserID")]
        public int Admin { get; set; } // GET zjisti ID od zakladatele tymu
        [Column]
        public ICollection<User> Users { get; set; }
        [Column]
        public ICollection<Post> Posts { get; set; }
    }
}
