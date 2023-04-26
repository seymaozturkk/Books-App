using BooksApp.Entity.Concrete;
using BooksApp.MVC.Models.ViewModels;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsApproved { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Url { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}
