namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }
    }
}
