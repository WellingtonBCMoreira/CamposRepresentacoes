using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Repositories;
using CamposRepresentacoes.Services;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add services for your custom services and repositories
builder.Services.AddScoped<IProdutosService, ProdutoService>();
builder.Services.AddScoped<IProdutosRepository, ProdutoRepository>();

builder.Services.AddScoped<IClientesService, ClienteService>();
builder.Services.AddScoped<IClientesRepository, ClienteRepository>();

builder.Services.AddScoped<IFornecedoresService, FornecedorService>();
builder.Services.AddScoped<IFornecedoresRepository, FornecedorRepository>();

builder.Services.AddScoped<ITransportadorasService, TransportadoraService>();
builder.Services.AddScoped<ITransportadorasRepository, TransportadoraRepository>();

builder.Services.AddScoped<IPedidosService, PedidoService>();
builder.Services.AddScoped<IPedidosRepository, PedidoRepository>();

builder.Services.AddTransient<IEmailService, EmailService>();

// Configure Razor view engine to locate views in the specified paths
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/Pages/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
});

// Add DinkToPdf service
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddTransient<PDFService>();

// Add controllers with views and enable runtime compilation
builder.Services.AddControllersWithViews();

// Add Endpoint Routing
builder.Services.AddRouting();

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

// Migrate the database
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Database migration applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying database migration: {ex.Message}");
    }
}

app.MapRazorPages();
app.MapControllers();

app.Run();
