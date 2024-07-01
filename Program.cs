using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Repositories;
using CamposRepresentacoes.Services;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Account/AcessoNegado";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ActiveUser", policy =>
        policy.Requirements.Add(new ActiveUserRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, ActiveUserHandler>();

builder.Services.AddSession();
builder.Services.AddServerSideBlazor();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

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
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.MapControllers();

app.Run();
