using AIBrete.Client.Pages;
using AIBrete.Client.Service;
using AIBrete.Components;
using AIBrete.Service.Services;
using AIBrete.Shared.Configuration;
using AIBrete.Shared.Service.Interfaces;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredModal();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddHttpClient<IVacanteService, VacanteService>();
builder.Services.AddHttpClient<ICvService, CvServiceLocal>();

builder.Services.Configure<ConfigurationOptions>(
    builder.Configuration.GetSection("Configuracion"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AIBrete.Client._Imports).Assembly);

app.Run();
