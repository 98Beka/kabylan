using AutoMapper;
using Kabylan.BLL.Services;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Repository;
using KabylanMVC.PL.Profiles;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionStr = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddSingleton<SaleService, SaleService>(
    s => new SaleService(connectionStr));
builder.Services.AddSingleton<IUnitOfWork>(new EFUnitOfWork(connectionStr));
builder.Services.AddSingleton<IMapper, IMapper>(
    m => new MapperConfiguration(c => {
        c.AddProfile<MapperConfig>();
    }).CreateMapper());


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
