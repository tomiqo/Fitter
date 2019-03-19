using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
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
        public ICollection<Team> Teams { get; set; }
    }
}
