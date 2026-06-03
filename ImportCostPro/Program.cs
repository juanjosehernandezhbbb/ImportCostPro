using ImportCostPro.Application.Interfaces;
using ImportCostPro.Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configuración de cultura
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración de Antiforgery (Esto asegura que tus formularios sean seguros)
builder.Services.AddAntiforgery(options => {
    options.HeaderName = "X-CSRF-TOKEN";
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ImportCostProContext>(options =>
    options.UseSqlServer(connectionString));

// Inyección de dependencias
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IMonedaService, MonedaService>();
builder.Services.AddScoped<IConfigImpuestosService, ConfigImpuestosService>();
builder.Services.AddScoped<IImportadorService, ImportadorService>();
builder.Services.AddScoped<ICategoriaArancelariaService, CategoriaArancelariaService>();
builder.Services.AddScoped<IDetalleOrdenService, DetalleOrdenService>();
builder.Services.AddScoped<IGastoImportacionService, GastoImportacionService>();
builder.Services.AddScoped<ILandedCostService, LandedCostService>();
builder.Services.AddScoped<IOrdenImportacionService, OrdenImportacionService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ITasaCambioService, TasaCambioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// MapStaticAssets para .NET 9
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();