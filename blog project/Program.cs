global using blog_project.Data;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configurationmanager = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddDbContext<blogContext>(options =>
{
    options.UseSqlite($"Filename={configurationmanager.GetConnectionString("sqlite")}");
});
builder.Services.AddDbContext<userContext>(options =>
{
    options.UseSqlite($"Filename={configurationmanager.GetConnectionString("sqlite")}");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
