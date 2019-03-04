using System;
using System.Collections.Generic;
using System.Text;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class Users : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }
        public Enums.Status LoggedIn { get; set; }
        public string LastActivity { get; set; } // cas posledního Akce ( komentu, příspěvku, ...)
    }
}
