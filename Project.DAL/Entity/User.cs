using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Project.DAL.Entity.Base;

namespace Project.DAL.Entity
{   
    [Table("User")]
    public class User : BaseEntity
    {
        [Column]
        public string Name { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string Password
        {
            get { return passwd;}
            set { passwd = VratHash(value);}
        }
        [Column]
        public string Nick { get; set; }
        [Column]
        public ICollection<UsersInTeam> TeamOfUsers { get; set; } // kvoli M-to-N
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Post> Posts { get; set; }

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