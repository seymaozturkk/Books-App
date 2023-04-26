using BooksApp.Business.Abstract;
using BooksApp.Data.Abstract;
using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task CreateAsync(Author author)
        {
           await _authorRepository.CreateAsync(author);  
        }

        public void Delete(Author author)
        {
            _authorRepository.Delete(author);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<List<Author>> GetAllAuthorsWithBooksAsync(bool ApprovedStatus)
        {
            return await _authorRepository.GetAllAuthorsWithBooksAsync(ApprovedStatus);
        }

        public async Task<List<Author>> GetAuthorsByBook(int id)
        {
            return await _authorRepository.GetAuthorsByBook(id);
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public void Update(Author author)
        {
            _authorRepository.Update(author);
        }
    }
}