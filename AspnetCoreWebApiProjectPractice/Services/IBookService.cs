using AspnetCoreWebApiProjectPractice.DTO.Book;
using AspnetCoreWebApiProjectPractice.Models;

namespace AspnetCoreWebApiProjectPractice.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(BookQueryParameters query);
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto createBookDto);
        Task<bool> UpdateAsync(int id, CreateBookDto createBookDto);
        Task<bool> DeletAsync(int id);
    }
}
