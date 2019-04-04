using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fitter.DAL.Entity.Base;

namespace Fitter.DAL.Entity
{   
    public class User : BaseEntity
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)] 
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "Email is missing.")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is missing.")]
        public ICollection<UsersInTeam> UsersInTeams { get; set; } = new List<UsersInTeam>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}