using System;
using System.Collections.Generic;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class Team : Users
    {
        public int MemberCount { get; set; }
        public string TeamName { get; set; }
        public string DateOfCreation { get; set; } //20.12.2019 , 12-20-2019 .. 
        public Guid Admin { get; set; } // GET zjisti ID od zakladatele tymu
    }
}
