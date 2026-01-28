using Microsoft.EntityFrameworkCore;
using SalaReunioes.Infrastructure.Data;
using SalaReunioes.Infrastructure.Services; // Adicione este using
using SalaReunioes.Web.Client.Pages;
using SalaReunioes.Web.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


// 1. Configurar a Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Registrar o DbContext (Infraestrutura)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. REGISTRAR O SERVIÇO DE AGENDAMENTO (O comando que você tentou rodar no terminal)
builder.Services.AddScoped<AgendamentoService>();

// 4. Registrar os serviços do MudBlazor
builder.Services.AddMudServices();

// 5. Adicionar serviços do Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure o pipeline de requisições HTTP
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