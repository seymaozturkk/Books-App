namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class AuthorListViewModel
    {
        public List<AuthorViewModel> Authors { get; set; }
        public bool ApprovedStatus { get; set; } = true;
    }
}
