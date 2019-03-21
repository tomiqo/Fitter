using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{
    public class UsersInTeam : BaseEntity
    {
        public User User { get; set; }

        public Team Team { get; set; }
    }
}
