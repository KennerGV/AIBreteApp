using AIBrete.Shared.Configuration;
using AIBrete.Shared.Model;
using AIBrete.Shared.Service.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

public class CvServiceLocal : ICvService
{
    private readonly HttpClient _httpClient;
    private readonly ConfigurationOptions _config;
    private readonly string _endpointPath = "api/CvAPI/upload";

    public CvServiceLocal(HttpClient httpClient, IOptions<ConfigurationOptions> configOptions)
    {
        _httpClient = httpClient;
        _config = configOptions.Value;
    }

    public async Task<CvData> UploadCvAsync1(IBrowserFile file)
    {
        var cv = new CvData();
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        // Construir URI final sin usar Trim
        var uriBuilder = new UriBuilder(_config.BackendApiUrl)
        {
            Path = _endpointPath
        };
        var finalUri = uriBuilder.Uri;

        // Preparar contenido multipart
        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024)); // 10MB límite
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        content.Add(fileContent, "CvFile", file.Name);

        var response = await _httpClient.PostAsync(finalUri, content);
        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            cv = JsonSerializer.Deserialize<CvData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return cv;
        }

        return null;

    }

    public async Task<CvData?> UploadCvAsync(IBrowserFile file)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        // Construcción segura del URI
        var uriBuilder = new UriBuilder(_config.BackendApiUrl)
        {
            Path = _endpointPath
        };
        var finalUri = uriBuilder.Uri;

        // Preparar contenido multipart
        using var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10MB límite
        using var fileContent = new StreamContent(stream);

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        content.Add(fileContent, "CvFile", file.Name);

        // Definir timeout por petición (5 minutos)
        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

        try
        {
            var response = await _httpClient.PostAsync(finalUri, content, cts.Token);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cts.Token);
            var cv = JsonSerializer.Deserialize<CvData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return cv ?? new CvData();
        }
        catch (TaskCanceledException) when (!cts.IsCancellationRequested)
        {
            throw new TimeoutException("La petición fue cancelada por exceder el tiempo límite.");
        }
        catch (HttpRequestException ex)
        {
            // Podés loguear o manejar errores de red aquí
            throw new Exception("Error al enviar el archivo al backend.", ex);
        }
    }
}
