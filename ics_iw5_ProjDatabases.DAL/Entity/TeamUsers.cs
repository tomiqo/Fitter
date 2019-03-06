using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class TeamUsers
    {
        [Key]
        [Column]
        public int TeamUsersID { get; set; }

        [Column]
        [ForeignKey("UsersID")]
        public int MyUsers { get; set; }
    }
}
