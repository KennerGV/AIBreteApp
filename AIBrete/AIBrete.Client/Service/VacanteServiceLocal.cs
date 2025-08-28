using AIBrete.Shared.Configuration;
using AIBrete.Shared.Model;
using AIBrete.Shared.Service.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AIBrete.Client.Service
{
    public class VacanteServiceLocal : IVacanteService
    {

        private readonly HttpClient _httpClient;
        private readonly ConfigurationOptions _config;
        private readonly string _endpointBackEnd;

        public VacanteServiceLocal(HttpClient httpClient, IOptions<ConfigurationOptions> configOptions)
        {
            _httpClient = httpClient;
            _config = configOptions.Value;
            _endpointBackEnd = "api/JobAPI";
        }

        public async Task<IEnumerable<Vacante>> GetVacantes(string searchTerm, int minCompatibilidad, bool ascendente)
        {
            // 1. Obtener la URL base del config
            var uriBase = _config.BackendApiUrl;

            // 2. Usar UriBuilder para armar la URL completa
            var builder = new UriBuilder(uriBase)
            {
                Path = _endpointBackEnd
            };

            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            query["searchTerm"] = searchTerm;
            query["minCompatibilidad"] = minCompatibilidad.ToString();
            query["ascendente"] = ascendente.ToString().ToLower();
            builder.Query = query.ToString();

            var finalUri = builder.Uri;

            // 3. Realizar la petición con el HttpClient
            var response = await _httpClient.GetAsync(finalUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Vacante>>();
        }

        public void VerDetalles(string titulo)
        {
            // Aquí puedes mostrar detalles o navegar, por ejemplo
            Console.WriteLine($"Ver detalles de: {titulo}");
        }

        public void Postular(string titulo)
        {
            // Simulación de postulación
            Console.WriteLine($"Postulado automáticamente a: {titulo}");
        }
    }
}
