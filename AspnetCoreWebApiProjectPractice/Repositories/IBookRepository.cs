using AspnetCoreWebApiProjectPractice.Models;

namespace AspnetCoreWebApiProjectPractice.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        IQueryable<Book> Query();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        void Update(Book book);
        void Delete(Book book);
        Task<bool> SaveChangesAsync();
    }
}
