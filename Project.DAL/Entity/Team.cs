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
        public int Admin { get; set; }
        [Column]
        //public ICollection<User> Users { get; set; }
        public ICollection<UsersInTeam> UsersInTeams { get; set; } // kvoli M-to-N
        [Column]
        public ICollection<Post> Posts { get; set; }
    }
}
