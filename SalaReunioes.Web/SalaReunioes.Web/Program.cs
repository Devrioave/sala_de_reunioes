using Microsoft.EntityFrameworkCore;
using SalaReunioes.Infrastructure.Data; // Se o erro CS0234 persistir aqui, siga o passo 2
using SalaReunioes.Web.Client.Pages;
using SalaReunioes.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar a Connection String (Lida do appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Registrar o DbContext da camada de Infrastructure
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SalaReunioes.Web.Client._Imports).Assembly);

app.Run();