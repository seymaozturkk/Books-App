using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Business.Abstract
{
    public interface IAuthorService
    {
        Task CreateAsync(Author author);
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        void Update(Author author);
        void Delete(Author author);
        Task<List<Author>> GetAllAuthorsWithBooksAsync(bool ApprovedStatus);
        Task<List<Author>> GetAuthorsByBook(int id);
    }
}
