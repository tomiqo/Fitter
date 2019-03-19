using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{
    [Table("Team")]
    public class Team : BaseEntity 
    {
        [Column]
        public int MemberCount { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public DateTime Created { get; set; } 
        [Column]
        [ForeignKey("UserID")]
        public int Admin { get; set; } // GET zjisti ID od zakladatele tymu
        [Column]
        public ICollection<User> Users { get; set; }
        [Column]
        public ICollection<Post> Posts { get; set; }
    }
}
