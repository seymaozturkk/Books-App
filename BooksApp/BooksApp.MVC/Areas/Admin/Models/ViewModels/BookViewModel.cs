using BooksApp.Entity.Concrete;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class BookViewModel
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
        public List<CategoryViewModel> Categories { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
        public List<Image> Images { get; set; }
        public string Url { get; set; }
    }
}
