using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Abstract
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        void ClearCart(int cartId);
        Task ChangeQuantityAsync(CartItem cartItem, int quantity);
    }
}
