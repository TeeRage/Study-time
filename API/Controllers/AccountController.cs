using System.Security.Cryptography; //For hashing SHA512
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        //Register new app user, uses our own RegisterDTO class
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //Check if user already exists in our database
            if(await UserExists(registerDto.UserName)) return BadRequest("This username is already taken");

            //Use SHA512 hashing algorithm to initialize new random key.
            using var hmac = new HMACSHA512();

            //Create new AppUser and create hash and salt based on user password
            var user = new AppUser {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            //Tell framework that we want to add this user to the users collection/table.
            _context.Users.Add(user);
            //Save the user tracked to the db
            await _context.SaveChangesAsync();

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        //Login user
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //Get user from db
            var user = await _context.Users.SingleOrDefaultAsync(usr => usr.UserName == loginDto.UserName);
            
            //Check if username can be found from db
            if(user == null) return Unauthorized("Invalid username");

            //Compute hash using user pw salt from db
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //check if input password matches to db pw
            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        //Check if username already exists
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
        }
    }
}