using AspnetCoreWebApiProjectPractice.Context;
using AspnetCoreWebApiProjectPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreWebApiProjectPractice.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly MyAppDbContext dbContext;

        public BookRepository(MyAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await dbContext.Books.ToListAsync();
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            return await dbContext.Books.FindAsync(id);
        }
        public async Task AddAsync(Book book)
        {
            await dbContext.Books.AddAsync(book);
        }
        public void Update(Book book)
        {
            dbContext.Books.Update(book);
        }

        public void Delete(Book book)
        {
            dbContext.Books.Remove(book);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }

        public IQueryable<Book> Query()
        {
            return dbContext.Books.AsQueryable();
        }
    }
}
