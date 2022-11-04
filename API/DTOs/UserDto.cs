//This Data Transfer Object is used fot user login using JWT tokens
namespace API.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        
    }
}