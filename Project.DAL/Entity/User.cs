using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{   
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password
        {
            get { return passwd;}
            set { passwd = VratHash(value);}
        }
        public string Nick { get; set; }
        public ICollection<UsersInTeam> UsersInTeams { get; set; } = new List<UsersInTeam>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        private string passwd;
        private string VratHash(string data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] ArrayOfsha256 = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                StringBuilder ReturnString = new StringBuilder();
                for (int i = 0; i < ArrayOfsha256.Length; i++)
                    ReturnString.Append(ArrayOfsha256[i].ToString("x2"));

                return ReturnString.ToString();
            }
        }
    }
}