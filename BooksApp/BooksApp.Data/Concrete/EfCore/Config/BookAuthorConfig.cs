using Microsoft.EntityFrameworkCore;
using BooksApp.Entity.Concrete;

namespace BooksApp.Data.Concrete.EfCore.Config
{
    public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(ba => new { ba.BookId, ba.AuthorId });
            builder.HasData(
                new BookAuthor { BookId = 1, AuthorId = 1 },
                new BookAuthor { BookId = 2, AuthorId = 1 },
                new BookAuthor { BookId = 2, AuthorId = 2 },
                new BookAuthor { BookId = 3, AuthorId = 3 },
                new BookAuthor { BookId = 4, AuthorId = 4 },
                new BookAuthor { BookId = 5, AuthorId = 5 },
                new BookAuthor { BookId = 6, AuthorId = 6 },
                new BookAuthor { BookId = 7, AuthorId = 2 },
                new BookAuthor { BookId = 8, AuthorId = 1 },
                new BookAuthor { BookId = 9, AuthorId = 3 },
                new BookAuthor { BookId = 10, AuthorId = 4 },
                new BookAuthor { BookId = 11, AuthorId = 5 },
                new BookAuthor { BookId = 12, AuthorId = 6 }
            );
        }
    }
}