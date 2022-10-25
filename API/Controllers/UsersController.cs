using API.Data; //For DataContext
using System.Threading.Tasks; //Async calls
using Microsoft.EntityFrameworkCore; //Async lists
using API.Entities;
using Microsoft.AspNetCore.Mvc; //For ControllerBase


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //Asynchronously get all users as a list using GetUsers() action //api/users/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()//IEnumerable list
        {
            return await _context.Users.ToListAsync();
        }

        //Get one specific user using GetUser() action //api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        { 
            return await _context.Users.FindAsync(id);
        }
    }
}