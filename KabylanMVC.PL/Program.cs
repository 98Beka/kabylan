using AutoMapper;
using Kabylan.BLL.Services;
using KabylanMVC.PL.Profiles;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<SaleService, SaleService>(
    s => new SaleService(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddTransient<IMapper, IMapper>(
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
