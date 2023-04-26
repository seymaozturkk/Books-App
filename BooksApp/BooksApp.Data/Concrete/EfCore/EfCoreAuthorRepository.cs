using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreAuthorRepository : EfCoreGenericRepository<Author>, IAuthorRepository
    {
        public EfCoreAuthorRepository(BooksAppContext _appContext) : base(_appContext)
        {
        }

        BooksAppContext AppContext
        {
            get { return _dbContext as BooksAppContext; }
        }

        public async Task<List<Author>> GetAllAuthorsWithBooksAsync(bool ApprovedStatus)
        {
            List<Author> result = await AppContext
                    .Authors
                    .Where(a => a.IsApproved == ApprovedStatus)
                    .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                    .ToListAsync();
            return result;
        }

        public async Task<List<Author>> GetAuthorsByBook(int id)
        {
            List<Author> result = await AppContext
                .Authors
                .Where(a => a.IsApproved == true)
                .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .Where(a => a.BookAuthors.Any(x => x.BookId == id))
                .ToListAsync();
            return result;
        }
    }
}