using System;
using System.Collections.Generic;
using Fitter.DAL.Entity.Base;

namespace Fitter.DAL.Entity
{
    public class Team : BaseEntity 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public User Admin { get; set; }
        public ICollection<UsersInTeam> UsersInTeams { get; set; } = new List<UsersInTeam>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
