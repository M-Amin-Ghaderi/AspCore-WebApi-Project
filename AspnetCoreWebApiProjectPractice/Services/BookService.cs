using AspnetCoreWebApiProjectPractice.DTO.Book;
using AspnetCoreWebApiProjectPractice.Models;
using AspnetCoreWebApiProjectPractice.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreWebApiProjectPractice.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repo;
        private readonly IMapper mapper;

        public BookService(IBookRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<BookDto>> GetAllAsync(BookQueryParameters query)
        {
            var bookQuery = repo.Query();
            if (!string.IsNullOrEmpty(query.Search))
            {
                bookQuery = bookQuery.Where(book => book.Title.Contains(query.Search));
            }
            bookQuery = bookQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize);

            var books = await bookQuery.ToListAsync();
            return mapper.Map<List<BookDto>>(books);
        }
        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await repo.GetByIdAsync(id);
            if (book is null) { return null; }
            return mapper.Map<BookDto>(book);
        }
        public async Task<BookDto> CreateAsync(CreateBookDto createBookDto)
        {
            var book = mapper.Map<Book>(createBookDto);
            await repo.AddAsync(book);
            await repo.SaveChangesAsync();
            return mapper.Map<BookDto>(book);
        }
        public async Task<bool> UpdateAsync(int id, CreateBookDto createBookDto)
        {
            var book = await repo.GetByIdAsync(id);
            if (book is null) return false;
            mapper.Map(createBookDto, book);
            repo.Update(book);
            return await repo.SaveChangesAsync();
        }
        public async Task<bool> DeletAsync(int id)
        {
            var book = await repo.GetByIdAsync(id);
            if (book is null) return false;
            repo.Delete(book);
            return await repo.SaveChangesAsync();
        }

    }
}
