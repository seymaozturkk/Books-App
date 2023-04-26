using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using BooksApp.Business.Abstract;
using BooksApp.Business.Concrete;
using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete.Identity;
using BooksApp.MVC.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BooksAppContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<BooksAppContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit= true;//Þifre içinde mutlaka rakam olmalý
    options.Password.RequireLowercase= true;//Þifre içinde mutlaka küçük harf olmalý
    options.Password.RequireUppercase = true;//Þifre içinde mutlaka büyük harf olmalý
    options.Password.RequiredLength= 6; //Uzunluðu 6 karakter olmalý
    options.Password.RequireNonAlphanumeric= true;//Alfanümeric olmayan karakter barýndýrmalý
    //Örnek geçerli parola: Qwe123.

    options.Lockout.MaxFailedAccessAttempts= 3;//Üst üste izin verilecek hatalý giriþ sayýsý 3
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(5);//Kilitlenmiþ hesaba 5 dakika sonra giriþ yapýlabilsin

    options.User.RequireUniqueEmail= true;//Sistemde daha önce kayýtlý olmayan bir email adresi ile kayýt olunabilsin
    options.SignIn.RequireConfirmedEmail= false;//Email onayý pasif 
    options.SignIn.RequireConfirmedPhoneNumber= false;//Telefon numarasý onayý pasif
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";//Eðer kullanýcý eriþebilmesi için login olmak zorunda olduðu bir yere istekte bulunursa, bu sayfaya yönlendirilecek. (account controlleri içindeki login actioný)
    options.LogoutPath= "/account/logout";//Kullanýcý logout olduðunda bu actiona yönlendirilecek.
    options.AccessDeniedPath = "/account/accessdenied";//Kullanýcý yetkisi olmayan bir sayfaya istekte bulunduðunda bu actiona yönlendirilecek.
    options.SlidingExpiration = true;//Cookie yaþam süresinin her istekte sýfýrlanmasýný saðlar. Default olarak yaþam süresi 20 dk, ama biz bunu ayarlayabiliriz.
    options.ExpireTimeSpan = TimeSpan.FromDays(10);//Yaþam süresi 10 gün olacak.
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Name = ".BooksApp.Security.Cookie"
    };
});

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<IAuthorService, AuthorManager>();
builder.Services.AddScoped<IImageService, ImageManager>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<ICartItemService, CartItemManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
builder.Services.AddScoped<IBookRepository, EfCoreBookRepository>();
builder.Services.AddScoped<IAuthorRepository, EfCoreAuthorRepository>();
builder.Services.AddScoped<IImageRepository, EfCoreImageRepository>();
builder.Services.AddScoped<ICartRepository, EfCoreCartRepository>();
builder.Services.AddScoped<ICartItemRepository, EfCoreCartItemRepository>();
builder.Services.AddScoped<IOrderRepository, EfCoreOrderRepository>();

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(options=> new SmtpEmailSender (
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:UserName"],
    builder.Configuration["EmailSender:Password"]
  ));

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();    

app.UseRouting();

app.UseAuthorization();

app.UseNotyf();


app.MapControllerRoute(
    name:"bookdetails",
    pattern:"bookdetails/{url}",
    defaults: new { controller="Home", action="BookDetails"}
    );

app.MapControllerRoute(
    name: "categories",
    pattern: "books/{categoryurl?}",
    defaults: new { controller="Home", action="Index" }
    );

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "admin/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.Run();
