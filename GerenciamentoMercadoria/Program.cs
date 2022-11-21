using GerenciamentoEntrada.Repository.Interface;
using GerenciamentoEntrada.Repository;
using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Repository;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IEntradaRepository, EntradaRepository>();
builder.Services.AddScoped<ISaidaRepository, SaidaRepository>();
builder.Services.AddScoped<IFabricanteRepository, FabricanteRepository>();
builder.Services.AddScoped<IMercadoriaRepository, MercadoriaRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();

var app = builder.Build();

IWebHostEnvironment env = app.Environment;
RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env);

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
