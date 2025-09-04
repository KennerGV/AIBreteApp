using AIBrete.Service.Services;
using AIBrete.Service.Services.Auth;
using AIBrete.Shared.Configuration;
using AIBrete.Shared.Service.Interfaces;
using AIBrete.Shared.Service.Interfaces.Auth;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;

namespace AIBrete.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de secciones fuertemente tipadas
            var config = configuration.GetSection("Configuracion").Get<ConfigurationOptions>();
            services.Configure<ConfigurationOptions>(configuration.GetSection("Configuracion"));

            // Registro de servicios de terceros
            services.AddBlazoredModal();

            // Servicios de tu dominio            
            services.AddHttpClient<IVacanteService, VacanteService>();
            services.AddHttpClient<ICvService, CvServiceLocal>();

            // Registro de servicios relacionados con autenticación
            services.AddAuthorizationCore();
            services.AddSingleton<ITokenContextHolder, TokenContextHolder>();
            services.AddScoped<CustomAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddTransient<JwtDelegatingHandler>();

            // Configuración del HttpClient para la API
            services.AddHttpClient("Auth", client =>
            {
                client.BaseAddress = new Uri(config.BackendApiUrl);
            })
            .AddHttpMessageHandler<JwtDelegatingHandler>();

            // Registro del servicio de autenticación
            services.AddHttpClient<AuthService>()
                .AddHttpMessageHandler<JwtDelegatingHandler>();

            return services;
        }
    }

}
