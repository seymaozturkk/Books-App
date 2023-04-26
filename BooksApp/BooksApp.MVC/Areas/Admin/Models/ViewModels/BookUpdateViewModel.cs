using BooksApp.Entity.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class BookUpdateViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Onaylı")]
        public bool IsApproved { get; set; }

        [DisplayName("Kitap Adı")]
        [Required(ErrorMessage = "Kitap adı boş bırakılmamalıdır")]
        [MinLength(5, ErrorMessage = "Kitap adı en az 5 karakter olmalıdır")]
        [MaxLength(100, ErrorMessage = "Kitap adı en fazla 100 karakter olmalıdır")]
        public string Name { get; set; }

        [DisplayName("Stok Miktarı")]
        [Required(ErrorMessage = "Stok Miktarı boş bırakılmamalıdır")]
        public int? Stock { get; set; }

        [DisplayName("Fiyat")]
        [Required(ErrorMessage = "Fiyat boş bırakılmamalıdır")]
        public decimal? Price { get; set; }

        [DisplayName("Sayfa Sayısı")]
        [Required(ErrorMessage = "Sayfa sayısı boş bırakılmamalıdır")]
        public int? PageCount { get; set; }

        [DisplayName("Basım Yılı")]
        [Required(ErrorMessage = "Basım Yılı boş bırakılmamalıdır")]
        public int? EditionYear { get; set; }

        [DisplayName("Baskı No")]
        [Required(ErrorMessage = "Baskı No bırakılmamalıdır")]
        public int? EditionNumber { get; set; }

        [Required(ErrorMessage = "En az bir kategori seçilmelidir")]
        public int[] SelectedCategories { get; set; }
        [Required(ErrorMessage = "En az bir yazar seçilmelidir")]
        public int[] SelectedAuthors { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
        public string Url { get; set; }

        [DisplayName("Kitap Özeti")]
        [Required(ErrorMessage = "Kitap Özeti boş bırakılmamalıdır")]
        [MinLength(5, ErrorMessage = "Kitap Özeti en az 5 karakter olmalıdır")]
        [MaxLength(1000, ErrorMessage = "Kitap Özeti en fazla 1000 karakter olmalıdır")]
        public string Summary { get; set; }

        [DisplayName("Resim")]
        public List<IFormFile> Images { get; set; }
        public List<Image> ImageList { get; set; }
        
    }
}
