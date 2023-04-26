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
    public class CartItemManager: ICartItemService
    {
        ICartItemRepository _cartItemRepository;

        public CartItemManager(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task ChangeQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            await _cartItemRepository.ChangeQuantityAsync(cartItem, quantity);
        }

        public void ClearCart(int cartId)
        {
            _cartItemRepository.ClearCart(cartId);
        }

        public void Delete(CartItem cartItem)
        {
            _cartItemRepository.Delete(cartItem);
        }

        public async Task<CartItem> GetByIdAsync(int id)
        {
            return await _cartItemRepository.GetByIdAsync(id);
        }
    }
}