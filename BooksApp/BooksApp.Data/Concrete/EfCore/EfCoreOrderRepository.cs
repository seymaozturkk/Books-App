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
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order>, IOrderRepository
    {
        public EfCoreOrderRepository(BooksAppContext _appContext) : base(_appContext)
        {
        }
        BooksAppContext AppContext
        {
            get { return _dbContext as BooksAppContext; }
        }
        public async Task<List<Order>> GetAllOrdersAsync(string userId = null, bool dateSort = false)
        {
            var orders = AppContext
                .Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(oi => oi.Images)
                .AsQueryable();
            if (dateSort)
            {
                orders = orders.OrderByDescending(o => o.OrderDate);
            }
            else
            {
                orders = orders.OrderBy(o => o.OrderDate);
            }
            if (!String.IsNullOrEmpty(userId))
            {
                orders = orders.Where(o => o.UserId == userId);
            }
            return await orders.ToListAsync();
        }

        public async Task<List<Order>> SearchOrderByUser(string keyword, bool dateSort=false)
        {
            var orders = AppContext
                .Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(oi => oi.Images)
                .Where(o=>o.NormalizedName.Contains(keyword))
                .AsQueryable();
            if (dateSort)
            {
                orders = orders.OrderByDescending(o => o.OrderDate);
            }
            else
            {
                orders = orders.OrderBy(o => o.OrderDate);
            }
            return await orders.ToListAsync();
        }
    }
}