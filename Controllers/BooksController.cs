using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationOfBooks.Api.Entities;
using RegistrationOfBooks.Api.Persistence;
using Serilog;

namespace RegistrationOfBooks.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly RegistrationBooksDbContext _context;

        public BooksController(RegistrationBooksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Book>), 200)]
        public async Task<IActionResult> GetAll() => Ok(await _context.Books.AsNoTracking().ToListAsync());

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(Book), 200)]
        public async Task<IActionResult> GetById(long id)
        {
            Log.Information("GetById is called");

            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Book), 201)]
        public async Task<IActionResult> Post(Book model)
        {
            Log.Information("Post is called");

            if (model == null)
                return BadRequest();

            _context.Books.Add(model);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(Book), 200)]
        public async Task<IActionResult> Put(long id, Book model)
        {
            Log.Information("Put is called");

            if (model == null)
                return BadRequest();

            model.Id = id;

            _context.Books.Update(model);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            Log.Information("Delete is called");

            var book = _context.Books.Find(id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{name:string}")]
        [ProducesResponseType(typeof(IList<Book>), 200)]
        public async Task<IActionResult> GetByName(string name)
        {
            var books = await _context.Books.Where(x => x.title.Contains(name)).ToListAsync();

            if (books == null)
                return NotFound();

            return Ok(books);
        }
    }
}
