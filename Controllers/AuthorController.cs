using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;
using BookStoreApi.Controllers.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(BookStoreDbContext context) : ControllerBase
    {
        private readonly BookStoreDbContext _context = context;

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            // TODO - since this is a public endpoint, the private information such as email address, phone and address should not be returned.
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Author/5
        [Authorize(Roles = "Admin,Publisher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            // TODO - the email address should only be visible to the admins and publishers. 
            // Therefore check the roles to make sure that the user is an admin or publisher.

            return author;
        }

        /// <summary>
        /// Updates an author's information.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAuthor"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAuthor(int id, [FromBody] JsonPatchDocument<UpdateAuthorDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            // get the existing author from the database
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            // apply the patch document to the author entity
            var authorDto = new UpdateAuthorDto
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Phone = author.Phone,
                Address = author.Address
            };
            patchDocument.ApplyTo(authorDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // update the author entity
            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            author.Email = authorDto.Email;
            author.Phone = authorDto.Phone;
            author.Address = authorDto.Address;

            // update the UpdatedAt field
            author.UpdatedAt = DateTime.UtcNow;

            // save the changes to the database
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        /// <summary>
        /// Creates a new author.
        /// A 201 Created status is returned if the author is successfully created.
        /// This endpoint requires authentication. We are using JWT authentication.
        /// </summary>
        /// <param name="createAuthor"></param>
        /// <returns>The created author</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDto createAuthorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                FirstName = createAuthorDto.FirstName,
                LastName = createAuthorDto.LastName,
                Email = createAuthorDto.Email,
                Phone = createAuthorDto.Phone,
                Address = createAuthorDto.Address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // DELETE: api/Author/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
