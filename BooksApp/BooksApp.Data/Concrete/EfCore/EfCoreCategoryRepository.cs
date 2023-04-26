using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        public EfCoreCategoryRepository(BooksAppContext _appContext) : base(_appContext)
        {
        }

        BooksAppContext AppContext
        {
            get { return _dbContext as BooksAppContext; }
        }
        public async Task<List<Category>> GetCategoriesAsync(bool ApprovedStatus)
        {
            return await AppContext
                .Categories
                .Where(c => c.IsApproved==ApprovedStatus)
                .ToListAsync();
        }

        public async Task<string> GetCategoryNameByUrlAsync(string url)
        {
            Category category = await AppContext
                .Categories 
                .Where(c => c.Url == url)
                .FirstOrDefaultAsync();
            return category.Name;
        }
    }
}