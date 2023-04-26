using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Business.Abstract
{
    public interface ICartService
    {
        Task InitializeCart(string userId);
        Task AddToCart(string userId,int bookId, int quantity);
        Task<Cart> GetByIdAsync(int id);
        Task<Cart> GetCartByUserId(string userId);
    }
}
