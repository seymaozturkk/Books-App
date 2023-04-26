using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart>, ICartRepository
    {
        public EfCoreCartRepository(BooksAppContext _appContext) : base(_appContext)
        {
        }
        BooksAppContext AppContext
        {
            get { return _dbContext as BooksAppContext; }
        }

        public async Task AddToCart(string userId, int bookId, int quantity)
        {
            var cart = await GetCartByUserId(userId);
            if(cart!=null)
            {
                var index = cart.CartItems.FindIndex(ci => ci.BookId == bookId);
                if(index<0)//Ürün daha önceden sepete eklenmemişse
                {
                    cart.CartItems.Add(new CartItem
                    {
                        BookId = bookId,
                        CartId = cart.Id,
                        Quantity = quantity
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }
                AppContext.Carts.Update(cart);
                await AppContext.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await AppContext
                .Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Book)
                .ThenInclude(ci=>ci.Images)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}