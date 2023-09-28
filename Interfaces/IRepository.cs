using RegistrationOfBooks.Api.Entities;

namespace RegistrationOfBooks.Api.Interfaces
{
    public interface IRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById();
    }
}
