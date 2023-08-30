using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationOfBooks.Api.Entities;
using RegistrationOfBooks.Api.Persistence;

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
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Book), 201)]
        public async Task<IActionResult> Post(Book model)
        {
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
            if (model == null)
                return BadRequest();

            model.Id = id;

            _context.Books.Update(model);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }
    }
}
