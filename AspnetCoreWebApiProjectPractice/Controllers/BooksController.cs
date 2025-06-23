using AspnetCoreWebApiProjectPractice.Context;
using AspnetCoreWebApiProjectPractice.DTO.Book;
using AspnetCoreWebApiProjectPractice.Models;
using AspnetCoreWebApiProjectPractice.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace AspnetCoreWebApiProjectPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MyAppDbContext dbContext;
        private readonly IBookRepository repo;
        private readonly IMapper mapper;

        public BooksController(IBookRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await repo.GetAllAsync();
            return mapper.Map<List<Book>>(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await repo.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Book>(book));
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (createBookDto is null) return BadRequest(ModelState);

            var book = mapper.Map<Book>(createBookDto);

            await repo.AddAsync(book);
            await repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != book.Id)
            {
                return BadRequest();
            }

            repo.Update(book);
            await repo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await repo.GetByIdAsync(id);
            if (book is null) return NotFound();

            repo.Delete(book);
            await repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
