//This Data Transfer Object is used when user logs in
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {
        [Required] //Mandatory fields
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}