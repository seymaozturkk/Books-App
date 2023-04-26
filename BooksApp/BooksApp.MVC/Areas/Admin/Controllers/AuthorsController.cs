using BooksApp.Business.Abstract;
using BooksApp.Core;
using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Entity.Concrete;
using BooksApp.MVC.Areas.Admin.Models.ViewModels;
using BooksApp.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BooksApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AuthorsController : Controller
    {
        private IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        #region Listeleme
        public async Task<IActionResult> Index(AuthorListViewModel authorListViewModel)
        {
            List<Author> authorList = await _authorService.GetAllAuthorsWithBooksAsync(authorListViewModel.ApprovedStatus);
            List<AuthorViewModel> authors = new List<AuthorViewModel>();
            foreach (var author in authorList)
            {
                authors.Add(new AuthorViewModel
                {
                    Id = author.Id,
                    Name = author.Name,
                    IsApproved = author.IsApproved,
                    Url = author.Url,
                    BirthDate=author.BirthDate,
                    Gender=author.Gender,
                    Books = author.BookAuthors.Select(ba=> new BookViewModel
                    {
                        Id=ba.Book.Id,
                        Name=ba.Book.Name,
                        EditionNumber=ba.Book.EditionNumber,
                        EditionYear=ba.Book.EditionYear,
                        Price = ba.Book.Price,
                        PageCount=ba.Book.PageCount
                    }).ToList()
                });
            }
            
            authorListViewModel.Authors = authors;
            return View(authorListViewModel);
        }
        public async Task<IActionResult> GetAuthorsByBook(int id)
        {
            List<Author> authorList = await _authorService.GetAuthorsByBook(id);
            List<AuthorViewModel> authors = new List<AuthorViewModel>();
            foreach (var author in authorList)
            {
                authors.Add(new AuthorViewModel
                {
                    Id = author.Id,
                    Name = author.Name,
                    CreatedDate = author.CreatedDate,
                    ModifiedDate = author.ModifiedDate,
                    IsApproved = author.IsApproved,
                    Books = author.BookAuthors.Select(ba => new BookViewModel
                    {
                        Id = ba.Author.Id,
                        Name = ba.Author.Name,
                        Url = ba.Author.Url
                    }).ToList()
                });
            }
            AuthorListViewModel authorListViewModel = new AuthorListViewModel
            {
                Authors = authors,
                ApprovedStatus = true
            };
            return View("Index", authorListViewModel);
        }

        #endregion
        #region Yeni Kayıt
        [HttpGet]
        public IActionResult Create()
        {
            AuthorAddViewModel authorAddViewModel = new AuthorAddViewModel();
            return View(authorAddViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorAddViewModel authorAddViewModel)
        {
            if (ModelState.IsValid)
            {
                Author author = new Author
                {
                    Name = authorAddViewModel.Name,
                    Gender = authorAddViewModel.Gender,
                    Url = Jobs.GetUrl(authorAddViewModel.Name),
                    IsApproved = authorAddViewModel.IsApproved,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    About=authorAddViewModel.About
                };
                await _authorService.CreateAsync(author);
                return RedirectToAction("Index");
            }
            return View(authorAddViewModel);
        }

        #endregion
        #region Kayıt Güncelleme
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Author author = await _authorService.GetByIdAsync(id);
            AuthorUpdateViewModel authorUpdateViewModel = new AuthorUpdateViewModel()
            {
                Id = author.Id,
                Name = author.Name,
                Gender = author.Gender,
                ModifiedDate = author.ModifiedDate,
                IsApproved = author.IsApproved,
                About=author.About
            };

            return View(authorUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorUpdateViewModel authorUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                Author author = await _authorService.GetByIdAsync(authorUpdateViewModel.Id);
                author.Name = authorUpdateViewModel.Name;
                author.Gender = authorUpdateViewModel.Gender;
                author.Url = Jobs.GetUrl(authorUpdateViewModel.Name);
                author.IsApproved = authorUpdateViewModel.IsApproved;
                author.ModifiedDate = DateTime.Now;
                author.About = authorUpdateViewModel.About;
                _authorService.Update(author);

                return RedirectToAction("Index");
            }
            return View(authorUpdateViewModel);
        }

        #endregion
        #region Kayıt Silme
        public async Task<IActionResult> Delete(int id)
        {
            Author deletedAuthor = await _authorService.GetByIdAsync(id);
            if (deletedAuthor != null)
            {
                _authorService.Delete(deletedAuthor);
            }

            return RedirectToAction("Index");
        }
        #endregion
        #region Onaylı
        public async Task<IActionResult> UpdateIsApproved(int id, bool ApprovedStatus)
        {
            Author author = await _authorService.GetByIdAsync(id);
            if (author != null)
            {
                author.IsApproved = !author.IsApproved;
                _authorService.Update(author);
            }
            AuthorListViewModel authorListViewModel = new AuthorListViewModel()
            {
                ApprovedStatus = ApprovedStatus
            };
            return RedirectToAction("Index", authorListViewModel);  
        }
        #endregion
    }
}
