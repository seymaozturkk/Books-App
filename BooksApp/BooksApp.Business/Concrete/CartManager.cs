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
    public class CartManager : ICartService
    {
        private ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task AddToCart(string userId, int bookId, int quantity)
        {
            await _cartRepository.AddToCart(userId, bookId, quantity);
        }

        public Task<Cart> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _cartRepository.GetCartByUserId(userId);
        }

        public async Task InitializeCart(string userId)
        {
            await _cartRepository.CreateAsync(new Cart { UserId = userId });
        }
    }
}