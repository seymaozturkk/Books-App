using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetAllOrdersAsync(string userId = null, bool dateSort = false);
        Task<List<Order>> SearchOrderByUser(string keyword, bool dateSort = false);
    }
}
