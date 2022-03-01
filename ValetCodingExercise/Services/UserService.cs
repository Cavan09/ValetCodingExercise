using ValetCodingExercise.Models;
using ValetCodingExercise.Interfaces;
using ValetCodingExercise.Entities;
using Microsoft.EntityFrameworkCore;

namespace ValetCodingExercise.Services
{
    public class UserService : IUserService
    {
        private ValetDbExampleContext _context;
        public UserService(ValetDbExampleContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var users = await _context.Users.Where(x => x.Username == username).ToListAsync();

            return users.FirstOrDefault();
        }
    }
}
