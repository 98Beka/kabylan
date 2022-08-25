using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Interfaces;
using Kabylan.BLL.Services;
using Kabylan.PL.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddTransient<IService<UserDTO>, UserService>( 
    s => new UserService(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddTransient<SaleService, SaleService>( 
    s => new SaleService(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddTransient<IMapper, IMapper>(
    m => new MapperConfiguration(c => {
        c.AddProfile<MapperConfig>();
    }).CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
