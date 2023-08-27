using Microsoft.EntityFrameworkCore;
using RegistrationOfBooks.Api.Entities;

namespace RegistrationOfBooks.Api.Persistence
{
    public class RegistrationBooksDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(e => e.Id);
        }
        public RegistrationBooksDbContext(DbContextOptions<RegistrationBooksDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
    }
}
