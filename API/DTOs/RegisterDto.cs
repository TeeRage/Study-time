//This Data Transfer Object hides password salts and hashes when registering the user
//When returning the user information we do not return the username and password as visible values 
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] //Mandatory fields
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}