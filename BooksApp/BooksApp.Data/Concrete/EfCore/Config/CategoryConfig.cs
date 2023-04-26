using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksApp.Data.Concrete.EfCore.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            //builder.Property(x => x.Description).IsRequired().HasMaxLength(500).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);

            builder.HasData(
                new Category { Id = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Edebiyat", Description = "Edebiyat türü burada", Url="edebiyat" },
                new Category { Id = 2, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Başvuru", Description = "Başvuru kitapları burada", Url="basvuru" },
                new Category { Id = 3, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Çocuk", Description = "Çocuk kitapları burada", Url="cocuk" },
                new Category { Id = 4, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Ders Kitabı", Description = "Ders kitapları burada", Url="ders-kitabi" }
            );

        }
    }
}