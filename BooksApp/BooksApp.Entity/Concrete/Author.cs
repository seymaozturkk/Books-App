using BooksApp.Entity.Abstract;

namespace BooksApp.Entity.Concrete
{
    public class Author : IBaseEntity, IBaseCommonEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsApproved { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Url { get; set; }
        public string About { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        
    }
}