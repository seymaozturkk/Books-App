using BooksApp.Entity.Concrete;

namespace BooksApp.Data.Abstract
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<List<Author>> GetAllAuthorsWithBooksAsync(bool ApprovedStatus);
        Task<List<Author>> GetAuthorsByBook(int id);
    }
}