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
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks([FromQuery] BookQueryParameters query)
        {
            return bookService.GetAllAsync(query) is Task<IEnumerable<BookDto>> books
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


        [HttpPost("upload")]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("فایل نامعتبر است");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{uniqueFileName}";

            return Ok(new { fileUrl });
        }
    }
}
