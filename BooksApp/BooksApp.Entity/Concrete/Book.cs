using BooksApp.Entity.Abstract;

namespace BooksApp.Entity.Concrete
{
    public class Book : IBaseEntity, IBaseCommonEntity
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
        public List<BookCategory> BookCategories { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<Image> Images { get; set; }
        
    }
}