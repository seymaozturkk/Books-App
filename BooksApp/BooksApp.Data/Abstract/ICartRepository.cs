using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Abstract
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task AddToCart(string userId, int bookId, int quantity);
        Task<Cart> GetCartByUserId(string userId);
    }
}
