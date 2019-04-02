using System;

namespace Fitter.DAL.Entity
{
    public class UsersInTeam
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }
}
