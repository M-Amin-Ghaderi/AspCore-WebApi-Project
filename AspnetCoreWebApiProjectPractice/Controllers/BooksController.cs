using AspnetCoreWebApiProjectPractice.DTO.Book;
using AspnetCoreWebApiProjectPractice.Models;
using AspnetCoreWebApiProjectPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreWebApiProjectPractice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            return bookService.GetAllAsync() is Task<IEnumerable<BookDto>> books
                ? Ok(await books)
                : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await bookService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (createBookDto is null) return BadRequest(ModelState);

            var book = await bookService.CreateAsync(createBookDto);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, BookDto book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != book.Id)
            {
                return BadRequest();
            }
            await bookService.UpdateAsync(id, new CreateBookDto
            {
                Title = book.Title,
                Author = book.Author
            });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var deleted = await bookService.DeletAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
