using BooksApp.Entity.Concrete;

namespace BooksApp.MVC.Models.ViewModels
{
    public class BookModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsApproved { get; set; }
        public string Name { get; set; }
        public int? Stock { get; set; }
        public decimal? Price { get; set; }
        public int? PageCount { get; set; }
        public int? EditionYear { get; set; }
        public int? EditionNumber { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
        public List<Image> Images { get; set; }
    }
}
