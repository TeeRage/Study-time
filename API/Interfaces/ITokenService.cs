//Interface for handling JSON web token (JWT) logic for user "sessions"
//This is not really mandatory, TokenService service could funtion without interface
//Reason for using this: makes testing easier, is part of best practices

using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}