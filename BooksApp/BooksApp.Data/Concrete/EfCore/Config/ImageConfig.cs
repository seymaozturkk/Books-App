using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Concrete.EfCore.Config
{
    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();

            builder.Property(x => x.Url).IsRequired().HasMaxLength(500);

            builder.HasData(
                new Image { Id = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "1.jpg", BookId = 1 },
                new Image { Id = 2, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "2.jpg", BookId = 2 },
                new Image { Id = 3, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "3.jpg", BookId = 3 },
                new Image { Id = 4, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "4.jpg", BookId = 4 },
                new Image { Id = 5, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "5.jpg", BookId = 5 },
                new Image { Id = 6, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "6.jpg", BookId = 6 },
                new Image { Id = 7, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "7.jpg", BookId = 7 },
                new Image { Id = 8, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "8.jpg", BookId = 8 },
                new Image { Id = 9, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "9.jpg", BookId = 9 },
                new Image { Id = 10, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "10.jpg", BookId = 10 },
                new Image { Id = 11, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "11.jpg", BookId = 11 },
                new Image { Id = 12, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "12.jpg", BookId = 12 },
                new Image { Id = 13, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Url = "222.jpg", BookId = 1 }

                );
        }
    }
}