using API.Data; //For DataContext
using System.Threading.Tasks; //Async calls
using Microsoft.EntityFrameworkCore; //Async lists
using API.Entities;
using Microsoft.AspNetCore.Mvc; //For ControllerBase
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    //Derive this controller from BaseAPIController, and inherit ControllerBase from there
    [Authorize] //All methods now need authorization
    public class UsersController : BaseAPIController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //Asynchronously get all users as a list using GetUsers() action //api/users/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()//as IEnumerable list
        {
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        //Get one specific user using GetUser() action //api/users/{username}
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }
    }
}