namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class BookListViewModel
    {
        public List<BookViewModel> Books { get; set; }
        public bool ApprovedStatus { get; set; } = true;
    }
}
