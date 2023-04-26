using BooksApp.Entity.Concrete;
using BooksApp.Entity.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Concrete.EfCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            //Şu kullanıcının kartını oluştur
            #region Rol Bilgileri
            List<Role> roles = new List<Role>
            {
                new Role{Name="Admin", Description="Yöneticiler", NormalizedName="ADMIN"},
                new Role{Name="User", Description="Kullanıcılar", NormalizedName="USER"}
            };
            modelBuilder.Entity<Role>().HasData(roles);
            #endregion
            #region Kullanıcı Bilgileri
            List<User> users = new List<User>
            {
                new User{FirstName="Deniz", LastName="Kartal", UserName="deniz", NormalizedUserName="DENIZ", Email="deniz@gmail.com", NormalizedEmail="DENIZ@GMAIL.COM", Gender="Erkek", DateOfBirth=new DateTime(1985,5,18), Address="Halilpaşa Konağı Ap. No:18 Beşiktaş", City="İstanbul", EmailConfirmed=true, NormalizedName="DENIZKARTAL" },
                new User{FirstName="Selin", LastName="Kanarya", UserName="selin", NormalizedUserName="SELIN", Email="selin@gmail.com", NormalizedEmail="SELIN@GMAIL.COM", Gender="Kadın", DateOfBirth=new DateTime(1989,8,11), Address="Sinanpaşa Cd Halime Hanım Ap. No:1 D:5 Kadıköy", City="İstanbul", EmailConfirmed=true, NormalizedName="SELINKANARYA" }
            };
            modelBuilder.Entity<User>().HasData(users);
            #endregion
            #region Parola İşlemleri
            var passwordHasher = new PasswordHasher<User>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Qwe123.");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Qwe123.");
            #endregion
            #region Rol Atama İşlemleri
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>{ UserId=users[0].Id, RoleId=roles.FirstOrDefault(r=>r.Name=="Admin").Id},
                new IdentityUserRole<string>{ UserId=users[1].Id, RoleId=roles.FirstOrDefault(r=>r.Name=="User").Id}
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion
            #region AlışVeriş Sepeti İşlemleri
            List<Cart> carts = new List<Cart>
            {
                new Cart{Id=1, UserId=users[0].Id},
                new Cart{Id=2, UserId=users[1].Id}
            };
            modelBuilder.Entity<Cart>().HasData(carts);
            #endregion
        }
    }
}