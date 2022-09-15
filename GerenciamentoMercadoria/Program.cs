using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); 

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer
    ("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=gerenciarMercadoriaDb;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IFabricanteRepository, FabricanteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
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
