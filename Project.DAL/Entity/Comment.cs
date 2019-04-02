using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{
    public class Comment : BasePost
    {
        public Guid CurrentPostId { get; set; }
        public Post Post { get; set; }
    }
}
