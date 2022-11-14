//Implements IUserRepository Interface, this is used as a "communicator" between Context and Controller
//Added in ApplicationServiceExtensions
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        //Get one member (appuser with photos) with username
        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider) //Using auto mapper to map properites
            .SingleOrDefaultAsync();
        }

        //Get list of members (appuser with photos)
        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider) //Using auto mapper to map properites
            .ToListAsync();
        }

        //Gets user by id
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        //Gets user by username
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos) //Add photos to user using loading
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        //Gets list of all users
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos) //Add photos to user using loading
            .ToListAsync();
        }

        //Check if changes have been saved in database
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; //True if more than 0 changes have been saved
        }

        //Mark entity as modified
        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}