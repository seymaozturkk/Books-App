using BooksApp.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Entity.Concrete
{
    public class Image : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsApproved { get; set; }
        public string Url { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}