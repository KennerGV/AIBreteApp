using AIBrete.Shared.Configuration;
using AIBrete.Shared.Service.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

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

    public async Task<string> UploadCvAsync(IBrowserFile file)
    {
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

        return await response.Content.ReadAsStringAsync();
    }
}
