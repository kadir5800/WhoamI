using Microsoft.EntityFrameworkCore;
using WhoamI.Business;
using WhoamI.Data.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
services.AddControllersWithViews();
services.AddDistributedMemoryCache();
services.AddSession();

string connectionString = builder.Configuration.GetConnectionString("WhoamIDbContext");
services.AddDbContext<WhoamIDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(connectionString);
});

services.AddWhoamIDataWithEntityFrameworkCollection();
services.AddWhoamIDataWithBusinessCollection();
builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "Dashboard",
    pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseMiddleware<SessionCheckMiddleware>();

app.Run();
