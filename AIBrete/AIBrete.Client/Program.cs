using AIBrete.Client.Service;
using AIBrete.Shared.Configuration;
using AIBrete.Shared.Service.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<IVacanteService, VacanteServiceLocal>();

builder.Services.Configure<ConfigurationOptions>(
    builder.Configuration.GetSection("Configuracion"));

builder.Services.AddScoped<IVacanteService, VacanteServiceLocal>();

await builder.Build().RunAsync();
