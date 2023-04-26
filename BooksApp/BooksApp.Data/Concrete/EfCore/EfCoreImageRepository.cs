using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreImageRepository : EfCoreGenericRepository<Image>, IImageRepository
    {
        public EfCoreImageRepository(BooksAppContext _appContext) : base(_appContext)
        {
        }
        BooksAppContext AppContext
        {
            get { return _dbContext as BooksAppContext; }
        }
        public int ImageCount(int bookId)
        {
            return AppContext.Images.Count(i=>i.BookId== bookId);
        }
    }
}