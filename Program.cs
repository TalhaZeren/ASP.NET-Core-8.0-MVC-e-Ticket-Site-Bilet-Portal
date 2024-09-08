using BiletPortal.Data;
using BiletPortal.Dto;
using BiletPortal.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("DefaultConnection"));

// Session Struct was imported. 
builder.Services.AddSession(options =>{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Waiting for 30 minutes.
    options.Cookie.IsEssential = true; 
});



// UserManager Struct was imported
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN"; // CSRF token'ý header içinde olacak
});

builder.Services.AddTransient<CardViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
    var tokens = antiforgery.GetAndStoreTokens(context);
    context.Response.Cookies.Append("X-CSRF-TOKEN", tokens.RequestToken!,
        new CookieOptions() { HttpOnly = false });
    await next.Invoke();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
