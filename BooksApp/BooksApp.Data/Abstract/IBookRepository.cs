using BooksApp.Entity.Concrete;

namespace BooksApp.Data.Abstract
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<Book>> GetAllBooksFullDataAsync(bool ApprovedStatus, string categoryurl);
        Task<Book> GetBookFullDataAsync(int id);
        Task<List<Book>> GetBooksByAuthor(int id);
        Task CreateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors, List<Image> Images);
        Task UpdateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors);
        Task<int> GetByUrlAsync(string url);
    }
}