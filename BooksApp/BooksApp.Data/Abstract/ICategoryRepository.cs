using BooksApp.Entity.Concrete;

namespace BooksApp.Data.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoriesAsync(bool ApprovedStatus);
        Task<string> GetCategoryNameByUrlAsync(string url);
    }
}