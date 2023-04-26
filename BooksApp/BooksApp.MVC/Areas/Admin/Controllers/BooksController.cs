using BooksApp.Business.Abstract;
using BooksApp.Core;
using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Entity.Concrete;
using BooksApp.MVC.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BooksController : Controller
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IImageService _imageService;
        private IAuthorService _authorService;

        public BooksController(IBookService bookService, ICategoryService categoryService, IImageService imageService, IAuthorService authorService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _imageService = imageService;
            _authorService = authorService;
        }


        #region Listeleme
        public async Task<IActionResult> Index(BookListViewModel bookListViewModel)
        {
            List<Book> bookList;
            if (bookListViewModel.Books == null)
            {
                bookList = await _bookService.GetAllBooksFullDataAsync(bookListViewModel.ApprovedStatus);
                List<BookViewModel> books = new List<BookViewModel>();
                foreach (var book in bookList)
                {
                    books.Add(new BookViewModel
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Stock = book.Stock,
                        Price = book.Price,
                        PageCount = book.PageCount,
                        EditionYear = book.EditionYear,
                        EditionNumber = book.EditionNumber,
                        CreatedDate = book.CreatedDate,
                        ModifiedDate = book.ModifiedDate,
                        IsApproved = book.IsApproved,
                        Categories = book.BookCategories.Select(bc => new CategoryViewModel
                        {
                            Id = bc.Category.Id,
                            Name = bc.Category.Name,
                            Url = bc.Category.Url
                        }).ToList(),
                        Authors = book.BookAuthors.Select(ba => new AuthorViewModel
                        {
                            Id = ba.Author.Id,
                            Name = ba.Author.Name,
                            Url = ba.Author.Url
                        }).ToList(),
                        Images = book.Images
                    });
                }
                bookListViewModel.Books = books;
            }
            return View(bookListViewModel);
        }
        public async Task<IActionResult> GetBooksByAuthor(int id)
        {
            List<Book> bookList = await _bookService.GetBooksByAuthor(id);
            List<BookViewModel> books = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                books.Add(new BookViewModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    Stock = book.Stock,
                    Price = book.Price,
                    PageCount = book.PageCount,
                    EditionYear = book.EditionYear,
                    EditionNumber = book.EditionNumber,
                    CreatedDate = book.CreatedDate,
                    ModifiedDate = book.ModifiedDate,
                    IsApproved = book.IsApproved,
                    Categories = book.BookCategories.Select(bc => new CategoryViewModel
                    {
                        Id = bc.Category.Id,
                        Name = bc.Category.Name,
                        Url = bc.Category.Url
                    }).ToList(),
                    Authors = book.BookAuthors.Select(ba => new AuthorViewModel
                    {
                        Id = ba.Author.Id,
                        Name = ba.Author.Name,
                        Url = ba.Author.Url
                    }).ToList(),
                    Images = book.Images
                });
            }
            BookListViewModel bookListViewModel = new BookListViewModel
            {
                Books = books,
                ApprovedStatus = true
            };
            return View("Index", bookListViewModel);
        }

        #endregion
        #region Yeni Kayıt
        [HttpGet]
        
        public async Task<IActionResult> Create()
        {
            BookAddViewModel bookAddViewModel = new BookAddViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(true),
                Authors = await _authorService.GetAllAsync()
            };

            return View(bookAddViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookAddViewModel bookAddViewModel)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book
                {
                    Name = bookAddViewModel.Name,
                    Stock = bookAddViewModel.Stock,
                    PageCount = bookAddViewModel.PageCount,
                    Price = bookAddViewModel.Price,
                    EditionNumber = bookAddViewModel.EditionNumber,
                    EditionYear = bookAddViewModel.EditionYear,
                    Url = Jobs.GetUrl(bookAddViewModel.Name),
                    IsApproved = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Summary=bookAddViewModel.Summary
                };
                List<Image> images = bookAddViewModel.Images.Select(i => new Image
                {
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsApproved = true,
                    Url = Jobs.UploadImage(i)
                }).ToList();

                await _bookService.CreateBook(book, bookAddViewModel.SelectedCategories, bookAddViewModel.SelectedAuthors, images);
                TempData["Message"] = Jobs.CreateMessage("Başarılı", "Kitap başarıyla kaydedilmiştir.", "success");
                return RedirectToAction("Index");
            }
            bookAddViewModel.Categories = await _categoryService.GetCategoriesAsync(true);
            bookAddViewModel.Authors = await _authorService.GetAllAsync();
            TempData["Message"] = Jobs.CreateMessage("HATA!", "Kayıt sırasında bir hata oluştu, kontrol edip yeniden deneyiniz!", "warning");
            return View(bookAddViewModel);
        }

        #endregion
        #region Kayıt Güncelleme
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _bookService.GetBookFullDataAsync(id);
            BookUpdateViewModel bookUpdateViewModel = new BookUpdateViewModel()
            {
                Id = book.Id,
                Name = book.Name,
                Stock = book.Stock,
                PageCount = book.PageCount,
                EditionNumber = book.EditionNumber,
                EditionYear = book.EditionYear,
                Price = book.Price,
                ModifiedDate = book.ModifiedDate,
                IsApproved = book.IsApproved,
                Summary=book.Summary,
                ImageList = book.Images.Select(i => new Image
                {
                    Id = i.Id,
                    Url = i.Url
                }).ToList(),
                SelectedCategories = book.BookCategories.Select(i => i.Category.Id).ToArray(),
                SelectedAuthors = book.BookAuthors.Select(i => i.Author.Id).ToArray()
            };
            bookUpdateViewModel.Categories = await _categoryService.GetCategoriesAsync(true);
            bookUpdateViewModel.Authors = await _authorService.GetAllAsync();
            return View(bookUpdateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(BookUpdateViewModel bookUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                Book book = await _bookService.GetBookFullDataAsync(bookUpdateViewModel.Id);
                book.Name = bookUpdateViewModel.Name;
                book.Url = Jobs.GetUrl(bookUpdateViewModel.Name);
                book.IsApproved = bookUpdateViewModel.IsApproved;
                book.ModifiedDate = DateTime.Now;
                book.Stock = bookUpdateViewModel.Stock;
                book.PageCount = bookUpdateViewModel.PageCount;
                book.EditionNumber = bookUpdateViewModel.EditionNumber;
                book.EditionYear = bookUpdateViewModel.EditionYear;
                book.Price = bookUpdateViewModel.Price;
                book.Summary = bookUpdateViewModel.Summary;

                if (bookUpdateViewModel.Images != null)
                {
                    List<Image> images = bookUpdateViewModel.Images.Select(i => new Image
                    {
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsApproved = true,
                        Url = Jobs.UploadImage(i),
                        BookId = book.Id
                    }).ToList();
                    foreach (var item in images)
                    {
                        await _imageService.CreateAsync(item);
                    }
                }
                else if (_imageService.ImageCount(book.Id) == 0)
                {
                    bookUpdateViewModel.Categories = await _categoryService.GetCategoriesAsync(true);
                    bookUpdateViewModel.Images = new List<IFormFile>();
                    bookUpdateViewModel.ImageList = new List<Image>();

                    return View(bookUpdateViewModel);
                }

                await _bookService.UpdateBook(book, bookUpdateViewModel.SelectedCategories, bookUpdateViewModel.SelectedAuthors);
                return RedirectToAction("Index");
            }
            bookUpdateViewModel.Categories = await _categoryService.GetCategoriesAsync(true);
            bookUpdateViewModel.Authors = await _authorService.GetAllAsync();
            if (bookUpdateViewModel.ImageList == null) bookUpdateViewModel.ImageList = new();
            bookUpdateViewModel.Images = new();
            return View(bookUpdateViewModel);
        }

        #endregion
        #region Kayıt Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Book deletedBook = await _bookService.GetBookFullDataAsync(id);
            BookViewModel bookViewModel = new BookViewModel
            {
                Id = deletedBook.Id,
                Name = deletedBook.Name,
                CreatedDate = deletedBook.CreatedDate,
                ModifiedDate = deletedBook.ModifiedDate,
                EditionNumber = deletedBook.EditionNumber,
                EditionYear = deletedBook.EditionYear,
                Price = deletedBook.Price,
                Stock = deletedBook.Stock,
                PageCount = deletedBook.PageCount,
                IsApproved = deletedBook.IsApproved,
                Images = deletedBook.Images.Select(i => new Image
                {
                    Url = i.Url
                }).ToList()
            };
            List<Category> categoryList = deletedBook.BookCategories.Select(bc => bc.Category).ToList();
            bookViewModel.Categories = categoryList.Select(c => new CategoryViewModel
            { Name = c.Name }).ToList();
            List<Author> authorList = deletedBook.BookAuthors.Select(ba => ba.Author).ToList();
            bookViewModel.Authors = authorList.Select(a => new AuthorViewModel { Name = a.Name }).ToList();

            return View(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BookViewModel bookViewModel)
        {
            Book deletedBook = await _bookService.GetByIdAsync(bookViewModel.Id);
            if (deletedBook != null)
            {
                _bookService.Delete(deletedBook);
            }
            TempData["Message"] = Jobs.CreateMessage("Başarılı!", $"{deletedBook.Name} adlı kitap başarıyla silinmiştir!", "success");
            return RedirectToAction("Index");
        }
        #endregion
        #region Onaylı
        public async Task<IActionResult> UpdateIsApproved(int id, bool ApprovedStatus)
        {
            Book book = await _bookService.GetByIdAsync(id);
            if (book != null)
            {
                book.IsApproved = !book.IsApproved;
                _bookService.Update(book);
            }
            BookListViewModel bookListViewModel = new BookListViewModel
            {
                ApprovedStatus = ApprovedStatus
            };
            return RedirectToAction("Index", bookListViewModel);
        }
        #endregion
        #region Resim Silme
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, int bookId)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image != null)
            {
                _imageService.Delete(image);
            }
            //        return RedirectToAction("Edit", new RouteValueDictionary(
            //new { area="Admin",  controller = "Books", action = "Edit", Id = bookId }));
            return RedirectToAction("Edit", new { id = bookId });
        }
        #endregion
    }
}
