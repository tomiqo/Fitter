using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{
    public class Team : BaseEntity 
    {
        public string Name { get; set; }
        public DateTime Created { get; set; } 
        public Guid? Admin { get; set; }
        public ICollection<UsersInTeam> UsersInTeams { get; set; } = new List<UsersInTeam>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
