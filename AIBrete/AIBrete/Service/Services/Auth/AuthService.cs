using AIBrete.Model;
using AIBrete.Shared.Configuration;
using AIBrete.Shared.Model;
using AIBrete.Shared.Service.Interfaces.Auth;
using Microsoft.Extensions.Options;

namespace AIBrete.Service.Services.Auth
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenContextHolder _tokenContext;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
        private readonly ConfigurationOptions _config;
        private readonly string _endpointPath = "api/auth/login";
        private readonly string _endpointUserPath = "api/User/CreateUser";
        public AuthService(HttpClient httpClient, ITokenContextHolder tokenContext, CustomAuthenticationStateProvider authenticationStateProvider, IOptions<ConfigurationOptions> config)
        {
            _httpClient = httpClient;
            _tokenContext = tokenContext;
            _authenticationStateProvider = authenticationStateProvider;
            _config = config.Value;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var uriBuilder = new UriBuilder(_config.BackendApiUrl)
            {
                Path = _endpointPath
            };
            var finalUri = uriBuilder.Uri;
            var loginRequest = new { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync(finalUri, loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                _tokenContext.Token = result.Token;
                _authenticationStateProvider.MarkUserAsAuthenticated(result.Token);
                return true;
            }

            return false;
        }
        public async Task<RegisterResponse> RegisterAsync(UserModel user)
        {
            var uriBuilder = new UriBuilder(_config.BackendApiUrl)
            {
                Path = _endpointUserPath
            };
            var finalUri = uriBuilder.Uri;
            //var loginRequest = new { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync(finalUri, user);

            var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
                //_tokenContext.Token = result.Token;
                //_authenticationStateProvider.MarkUserAsAuthenticated(result.Token);

            return result;
        }

        public async Task LogoutAsync()
        {
            _tokenContext.Token = null;
            _authenticationStateProvider.MarkUserAsLoggedOut();
        }
        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(_tokenContext.Token);
        }
    }

}
