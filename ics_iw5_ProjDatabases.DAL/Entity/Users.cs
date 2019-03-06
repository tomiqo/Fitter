using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ics_iw5_ProjDatabases.DAL.Entity
{   
    [Table("Users")]
    class Users 
    {
        [Key]
        [Column]
        public int UserID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public string Nick { get; set; }
        [Column] // TODO : Maybe not neccessary
        public Enums.Status LoggedIn { get; set; }
        [Column]
        public DateTime LastActivity { get; set; } // cas posledního Akce ( komentu, příspěvku, ...)
        [Column]
        //Pointer to table of teams
        public int TableOfTeams { get; set; }
    }
}
