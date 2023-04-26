using BooksApp.Business.Abstract;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Entity.Concrete;
using BooksApp.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BooksApp.MVC.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = await _categoryService.GetCategoriesAsync(true);

            List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();
            foreach (Category category in categories)
            {
                categoryViewModelList.Add(new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Url = category.Url
                });
            }
            if (RouteData.Values["categoryurl"] != null)
            {
                ViewBag.SelectedCategory = RouteData.Values["categoryurl"];
            }
            return View(categoryViewModelList);
        }
    }
}
