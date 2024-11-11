using AlbertCalculator.Data;
using AlbertCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbertCalculator.Repositories
{
    public class UserRepository
    {
        private readonly AlbertCalculatorDataContext _context;

        public UserRepository(AlbertCalculatorDataContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> FindAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
