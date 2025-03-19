using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class AuthRepository
    {
        private readonly AddressBookDbContext _context;

        public AuthRepository(AddressBookDbContext context)
        {
            _context = context;
        }

        // Register user with hashed password
        public UserEntity Register(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        // Find user by email
        public UserEntity GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
