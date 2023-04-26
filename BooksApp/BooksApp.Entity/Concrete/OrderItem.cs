using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Entity.Concrete
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}