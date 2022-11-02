using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Class for users inside this app. Acts as a "schema" for database table via Entity framework.
namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt { get; set; }
    }
}