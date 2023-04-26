using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BooksApp.MVC.Views.Shared
{
    public class _MenuPartial : PageModel
    {
        private readonly ILogger<_MenuPartial> _logger;

        public _MenuPartial(ILogger<_MenuPartial> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}