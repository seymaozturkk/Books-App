using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Business.Abstract
{
    public interface IBookService
    {
        Task CreateAsync(Book book);
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync();
        void Update(Book book);
        void Delete(Book book);
        Task<List<Book>> GetAllBooksFullDataAsync(bool ApprovedStatus, string categoryurl = null);
        Task<Book> GetBookFullDataAsync(int id);
        Task<List<Book>> GetBooksByAuthor(int id);
        Task CreateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors, List<Image> Images);
        Task UpdateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors);
        Task<int> GetByUrlAsync(string url);

    }
}
