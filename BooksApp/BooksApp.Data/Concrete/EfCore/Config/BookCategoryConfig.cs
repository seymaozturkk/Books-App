using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksApp.Data.Concrete.EfCore.Config
{
    public class BookCategoryConfig : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new { bc.BookId, bc.CategoryId });
            builder.HasData(
                new BookCategory { BookId = 1, CategoryId = 1 },
                new BookCategory { BookId = 2, CategoryId = 1 },
                new BookCategory { BookId = 3, CategoryId = 1 },
                new BookCategory { BookId = 4, CategoryId = 1 },
                new BookCategory { BookId = 5, CategoryId = 1 },
                new BookCategory { BookId = 6, CategoryId = 1 },
                new BookCategory { BookId = 7, CategoryId = 1 },

                new BookCategory { BookId = 8, CategoryId = 2 },
                new BookCategory { BookId = 9, CategoryId = 2 },
                new BookCategory { BookId = 10, CategoryId = 2 },
                new BookCategory { BookId = 11, CategoryId = 2 },

                new BookCategory { BookId = 12, CategoryId = 3 },

                new BookCategory { BookId = 11, CategoryId = 4 },
                new BookCategory { BookId = 10, CategoryId = 4 },
                new BookCategory { BookId = 9, CategoryId = 4 },
                new BookCategory { BookId = 8, CategoryId = 4 },
                new BookCategory { BookId = 12, CategoryId = 4 }
            );
        }
    }
}