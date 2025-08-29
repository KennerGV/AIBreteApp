using AIBrete.Client.Service;
using AIBrete.Shared.Configuration;
using AIBrete.Shared.Service.Interfaces;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredModal();
builder.Services.AddHttpClient<IVacanteService, VacanteServiceLocal>();
builder.Services.AddHttpClient<ICvService, CvServiceLocal>();


builder.Services.Configure<ConfigurationOptions>(
    builder.Configuration.GetSection("Configuracion"));

await builder.Build().RunAsync();
