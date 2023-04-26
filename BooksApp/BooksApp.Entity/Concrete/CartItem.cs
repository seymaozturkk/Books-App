using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Entity.Concrete
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
    }
}