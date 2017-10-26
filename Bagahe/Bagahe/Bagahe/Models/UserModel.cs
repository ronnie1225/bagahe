using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace Bagahe.Models
{
    [Table("UserModel")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Name { get; set; }
    }
}


