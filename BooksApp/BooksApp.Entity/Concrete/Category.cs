using BooksApp.Entity.Abstract;

namespace BooksApp.Entity.Concrete
{
    public class Category : IBaseEntity, IBaseCommonEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsApproved { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<BookCategory> BookCategories { get; set; }
    }
}