using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    [Table("Team")]
    class Team 
    {
        [Key]
        [Column]
        public int TeamID { get; set; }
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
        //Pointer to table of team users
        public int TableOfUsers { get; set; }
        [Column]
        //Pointer to table of posts
        public int TableOfPosts { get; set; }
    }
        
}
