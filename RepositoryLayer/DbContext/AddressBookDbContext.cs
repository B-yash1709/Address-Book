using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AddressBookEntity;

namespace RepositoryLayer.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AddressBookEntity.AddressBookEntity> Contacts { get; set; } // Define your entities
}
