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
    public class BookManager : IBookService
    {
        private IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task CreateAsync(Book book)
        {
             await _bookRepository.CreateAsync(book);
        }

        public async Task CreateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors, List<Image> Images)
        {
            await _bookRepository.CreateBook(book, SelectedCategories, SelectedAuthors, Images);
        }

        public void Delete(Book book)
        {
            _bookRepository.Delete(book);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<List<Book>> GetAllBooksFullDataAsync(bool ApprovedStatus, string categoryurl=null)
        {
            return await _bookRepository.GetAllBooksFullDataAsync(ApprovedStatus, categoryurl);
        }

        public async Task<Book> GetBookFullDataAsync(int id)
        {
            return await _bookRepository.GetBookFullDataAsync(id);
        }

        public async Task<List<Book>> GetBooksByAuthor(int id)
        {
            return await _bookRepository.GetBooksByAuthor(id);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<int> GetByUrlAsync(string url)
        {
            return await _bookRepository.GetByUrlAsync(url);
        }

        public void Update(Book book)
        {
            _bookRepository.Update(book);
        }

        public async Task UpdateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors)
        {
            await _bookRepository.UpdateBook(book, SelectedCategories, SelectedAuthors);
        }
    }
}