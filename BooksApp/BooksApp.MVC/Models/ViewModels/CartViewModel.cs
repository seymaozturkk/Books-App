using System.ComponentModel.DataAnnotations;

namespace BooksApp.MVC.Models.ViewModels
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal? TotalPrice()
        {
            return CartItems.Sum(ci => ci.ItemPrice * ci.Quantity);
        }
    }
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookUrl { get; set; }
        public decimal? ItemPrice { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage ="Boş bırakılamaz")]
        [Range(1,10)]
        public int Quantity { get; set; }

    }
}
