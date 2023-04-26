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
    public class ImageManager : IImageService
    {
        private IImageRepository _imageRepository;

        public ImageManager(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task CreateAsync(Image image)
        {
            await _imageRepository.CreateAsync(image);
        }

        public void Delete(Image image)
        {
            _imageRepository.Delete(image);
        }

        public async Task<List<Image>> GetAllAsync()
        {
            return await _imageRepository.GetAllAsync();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await _imageRepository.GetByIdAsync(id);
        }

        public int ImageCount(int bookId)
        {
            return _imageRepository.ImageCount(bookId);
        }

        public void Update(Image image)
        {
            _imageRepository.Update(image);
        }
    }
}