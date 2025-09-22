using AIBrete.Service.Services.Auth;
using AIBrete.Shared.Model.DTO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public UserService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<UserDTO>?> GetUsersAsync()
    {
        var token = await _authService.GetTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetFromJsonAsync<List<UserDTO>>("api/users");
    }
}
