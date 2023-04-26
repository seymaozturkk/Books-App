using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Business.Abstract
{
    public interface ICategoryService
    {
        Task CreateAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        void Update(Category category);
        void Delete(Category category);
        Task<List<Category>> GetCategoriesAsync(bool ApprovedStatus);
        Task<string> GetCategoryNameByUrlAsync(string url);
    }
}
