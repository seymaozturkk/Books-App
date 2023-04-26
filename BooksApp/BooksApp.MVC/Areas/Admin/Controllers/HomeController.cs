using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BooksApp.Business.Abstract;
using BooksApp.MVC.Areas.Admin.Models.ViewModels;
//using BooksApp.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orderList = await _orderService.GetAllOrdersAsync(null, false);
            List<OrderViewModel> orders = orderList.Select(o => new OrderViewModel
            {
                Id = o.Id,
                Address = o.Address,
                City = o.City,
                Phone = o.Phone,
                Email = o.Email,
                FirstName = o.FirstName,
                LastName = o.LastName,
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel
                {
                    OrderItemId = oi.Id,
                    BookId = oi.BookId,
                    BookName = oi.Book.Name,
                    BookUrl = oi.Book.Url,
                    ImageUrl = oi.Book.Images[0].Url,
                    ItemPrice = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            }).ToList();
            return View(orders);
        }
        public async Task<IActionResult> SearchOrderByUser(string keyword)
        {
            var orderList = await _orderService.SearchOrderByUser(keyword.ToUpper());
            List<OrderViewModel> orders = orderList.Select(o => new OrderViewModel
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                City = o.City,
                Address = o.Address,
                Email = o.Email,
                Phone = o.Phone,
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel
                {
                    OrderItemId = oi.Id,
                    BookId = oi.BookId,
                    BookName = oi.Book.Name,
                    BookUrl = oi.Book.Url,
                    ImageUrl = oi.Book.Images[0].Url,
                    ItemPrice = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            }).ToList();
            return View("Index", orders);
        }
    }
}