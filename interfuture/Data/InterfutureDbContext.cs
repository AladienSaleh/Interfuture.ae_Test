using Microsoft.EntityFrameworkCore;

namespace interfuture.Data
{
    public class InterfutureDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
