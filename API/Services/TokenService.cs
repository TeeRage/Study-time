//Added to application startup class as a service and used to create JWT on user login in AccountController

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        //One key is used for encrypting and decrypting, JWT uses symmetric keys. 
        private readonly SymmetricSecurityKey _key;
        
        //Constructor, creates new key as byte array
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        //Create new JWT token for user
        public string CreateToken(AppUser user)
        {
            //Create claims for JWT
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            //Create credentials for JWT
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Description for new JWT token, "look" of the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), //JWT claims
                Expires = DateTime.Now.AddDays(7), //Expires after 7 days
                SigningCredentials = creds //JWT credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); //new token

            return tokenHandler.WriteToken(token);
        }
    }
}