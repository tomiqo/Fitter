using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fitter.DAL.Entity.Base
{
    public abstract class BasePost:BaseEntity
    {
        public Guid? CurrentAuthorId { get; set; }
        public User Author { get; set; }

        [Required]
        [StringLength(180, ErrorMessage = "{0} length can not be more than {1}.")]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public ICollection<User> Tags { get; set; } = new List<User>();
    }
}
