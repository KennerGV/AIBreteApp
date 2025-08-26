using AIBrete.Shared.Service.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AIBrete.Client.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IVacanteService, VacanteServiceLocal>();

await builder.Build().RunAsync();
