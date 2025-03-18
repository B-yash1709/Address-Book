using Microsoft.EntityFrameworkCore;
using RepositoryLayer.UserEntity;

namespace RepositoryLayer.Context
{
    public class AddressBookDbContext : DbContext
    {
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressBookEntity> AddressBookEntities { get; set; } // Define your entities
    }

}

