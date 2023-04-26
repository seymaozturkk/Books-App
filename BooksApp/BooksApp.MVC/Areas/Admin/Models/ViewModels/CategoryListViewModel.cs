namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class CategoryListViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public bool ApprovedStatus { get; set; } = true;
    }
}
