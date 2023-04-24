using Microsoft.EntityFrameworkCore;
using MvcDockerComics.Data;
using MvcDockerComics.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("MySql");
builder.Services.AddTransient<RepositoryComics>();
//MYSQL UTILIZA CONNECTION POOL Y TENEMOS DOS OPCIONES:
//1) PONER DE FORMA EXPLICITA LA VERSION DE MYSQL DENTRO DE CONNECTIONSTRING
//2) DETECTAR LA VERSION AUTOMATICA
builder.Services.AddDbContextPool<ComicsContext>
    (options => options.UseMySql(connectionString
    , ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
