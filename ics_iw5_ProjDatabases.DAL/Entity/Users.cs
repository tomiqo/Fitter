using System;
using System.Collections.Generic;
using System.Text;

namespace ics_iw5_ProjDatabases.DAL.Entity
{
    class Users : BaseEntity
    {
        public string Jmeno { get; set; }
        public string Email { get; set; }
        public string Heslo { get; set; }
        public string Nick { get; set; }
        public Enums.StavyPrihlaseni Prihlasen { get; set; }
        public string PosledniAktivita { get; set; }
    }
}
