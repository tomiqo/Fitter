using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.Entity.Base
{
    public class BasePost:BaseEntity
    {
        public Guid CurrentAuthorId { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public ICollection<User> Tags { get; set; } = new List<User>();
    }
}
