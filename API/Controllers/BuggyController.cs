//Controller for retrurning errors from various different responses
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        //Test for 401 unauthorized responses
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        //Test for 404 not found responses, tries to find user with id -1 
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);//This will be null

            if (thing == null) return NotFound();
            return Ok(thing);
        }


        //Test for 500 server error responses, tries to find user with id -1 
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1); //This will be null

            //We get null reference exception here
            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        //Test for bad request 400 responses
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}