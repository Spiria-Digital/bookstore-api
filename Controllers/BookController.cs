using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApi.Controllers
{
    /// <summary>
    /// Retrieve, create, update, and delete books.
    /// </summary>
    /// <param name="context"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(BookStoreDbContext context) : ControllerBase
    {
        private readonly BookStoreDbContext _context = context;

        /// <summary>
        /// Retrieves a list of books based on optional search parameters.
        /// If no search parameters are provided, all books are returned.
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="author"></param>
        /// <returns>A list of books matching the search criteria.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(
            [FromQuery] string? isbn,
            [FromQuery] string? title,
            [FromQuery] string? genre,
            [FromQuery] string? author
        )
        {
            // we are using AsQueryable to build the query dynamically
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .AsQueryable();

            if (!string.IsNullOrEmpty(isbn))
            {
                // we are doing partial search that's also case insensitive
                query = query.Where(b => b.ISBN.Contains(isbn, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(title))
            {
                // we are doing partial search that's also case insensitive
                query = query.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                // we are doing partial search that's also case insensitive
                query = query.Where(b => b.Genre != null &&
                    b.Genre.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(author))
            {
                // we are doing partial search that's also case insensitive on author's first or last name
                query = query.Where(b => b.Author != null &&
                    (b.Author.FirstName.Contains(author, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.LastName.Contains(author, StringComparison.OrdinalIgnoreCase)));
            }

            // TODO - we should only show minimal book info to unauthenticated users
            // TODO - only the author's first and last name should be shown

            var books = await query.ToListAsync();
            return books;
        }

        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        /// <summary>
        /// Updates a book by its ID.
        /// A 404 Not Found status is returned if the book does not exist.
        /// Other validation errors may also be returned.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book">The book to update.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new book.
        /// A 400 Bad Request status is returned if the book already exists.
        /// </summary>
        /// <param name="book">The book to create.</param>
        /// <returns>201 status upon success</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private Book? SearchBookByISBN(string isbn)
        {
            // search should be case insensitive
            return _context.Books.FirstOrDefault(b => b.ISBN.Equals(isbn, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
