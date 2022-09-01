using AutoMapper;
using Kabylan.BLL.Services;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionStr = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddSingleton<SaleService, SaleService>(
    s => new SaleService(connectionStr));
builder.Services.AddSingleton<CustomerService, CustomerService>(
    s => new CustomerService(connectionStr));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
