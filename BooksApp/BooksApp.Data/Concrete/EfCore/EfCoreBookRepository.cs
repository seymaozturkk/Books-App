using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreBookRepository : EfCoreGenericRepository<Book>, IBookRepository
    {
        public EfCoreBookRepository(BooksAppContext _appContext) : base(_appContext)
        {
        }

        BooksAppContext AppContext
        {
            get { return _dbContext as BooksAppContext; }
        }

        public async Task<List<Book>> GetAllBooksFullDataAsync(bool ApprovedStatus, string categoryurl = null)
        {
            var books = AppContext
                            .Books
                            .Where(b => b.IsApproved == ApprovedStatus)
                            .Include(b => b.BookCategories)
                            .ThenInclude(bc => bc.Category)
                            .AsQueryable();
            if (categoryurl != null)
            {
                books = books
                    .Where(b => b.BookCategories.Any(bc => bc.Category.Url == categoryurl));
            }

            return await books
                        .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                        .Include(b => b.Images)
                        .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByAuthor(int id)
        {
            List<Book> books = await AppContext
                .Books
                .Where(b => b.IsApproved == true)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Include(b => b.Images)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Where(b => b.BookAuthors.Any(x => x.AuthorId == id))
                .ToListAsync();
            return books;
        }

        public async Task CreateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors, List<Image> Images)
        {

            await AppContext.Books.AddAsync(book);
            await AppContext.SaveChangesAsync();
            List<BookCategory> bookCategories = new List<BookCategory>();
            foreach (var categoryId in SelectedCategories)
            {
                bookCategories.Add(new BookCategory
                {
                    CategoryId = categoryId,
                    BookId = book.Id
                });
            }
            AppContext.BookCategories.AddRange(bookCategories);

            List<BookAuthor> bookAuthors = new List<BookAuthor>();
            foreach (var authorId in SelectedAuthors)
            {
                bookAuthors.Add(new BookAuthor
                {
                    AuthorId = authorId,
                    BookId = book.Id
                });
            }
            AppContext.BookAuthors.AddRange(bookAuthors);
            foreach (var image in Images)
            {
                image.BookId = book.Id;
            }
            AppContext.Images.AddRange(Images);
            await AppContext.SaveChangesAsync();
        }

        public async Task<Book> GetBookFullDataAsync(int id)
        {
            var book = await AppContext
                            .Books
                            .Where(b => b.Id == id)
                            .Include(b => b.BookCategories)
                            .ThenInclude(bc => bc.Category)
                            .Include(b => b.BookAuthors)
                            .ThenInclude(ba => ba.Author)
                            .Include(b => b.Images)
                            .FirstOrDefaultAsync();
            return book;
        }

        public async Task UpdateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors)
        {
            Book newBook = AppContext
                .Books
                .Include(b => b.BookCategories)
                .Include(b => b.BookAuthors)
                .FirstOrDefault(b => b.Id == book.Id);
            newBook.Name = book.Name;
            newBook.CreatedDate= book.CreatedDate;
            newBook.ModifiedDate = DateTime.Now;
            newBook.PageCount=book.PageCount;
            newBook.Price = book.Price;
            newBook.EditionYear = book.EditionYear;
            newBook.EditionNumber = book.EditionNumber;
            newBook.Url = book.Url;
            newBook.IsApproved = book.IsApproved;
            newBook.Images = book.Images;

            newBook.BookCategories = SelectedCategories
                .Select(sc => new BookCategory
                {
                    BookId = newBook.Id,
                    CategoryId = sc
                }).ToList();
            newBook.BookAuthors = SelectedAuthors
                .Select(sa => new BookAuthor
                {
                    BookId = newBook.Id,
                    AuthorId = sa
                }).ToList();
            AppContext.Update(book);
            await AppContext.SaveChangesAsync();
        }

        public Task<int> GetByUrlAsync(string url)
        {
            var result = AppContext.Books.Where(b => b.Url == url).Select(b => b.Id).FirstOrDefaultAsync();
            return result;
        }
    }
}