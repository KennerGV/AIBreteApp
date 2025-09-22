using AIBrete.Components;
using AIBrete.Extensions;
using AIBrete.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// UI Components
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

// Servicios personalizados
builder.Services.AddCustomServices(builder.Configuration);

// Autenticación y Autorización con RS256
builder.Services.AddJwtRsaAuthentication(builder.Configuration);

builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TokenPopulationMiddleware>();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AIBrete.Client._Imports).Assembly);

app.Run();

