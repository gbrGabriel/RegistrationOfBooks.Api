using Microsoft.EntityFrameworkCore;
using RegistrationOfBooks.Api.Entities;
using RegistrationOfBooks.Api.Interfaces;
using RegistrationOfBooks.Api.Persistence;

namespace RegistrationOfBooks.Api.Repository
{
    public class Repository : IRepository
    {
        private readonly RegistrationBooksDbContext _context;
        public Repository(RegistrationBooksDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetAll()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public Task<Book> GetById()
        {
            throw new NotImplementedException();
        }
    }
}
