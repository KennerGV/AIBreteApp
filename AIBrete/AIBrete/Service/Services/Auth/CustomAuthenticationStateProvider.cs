using AIBrete.Shared.Service.Interfaces.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace AIBrete.Service.Services.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenContextHolder _tokenContext;

        public CustomAuthenticationStateProvider(ITokenContextHolder tokenContext)
        {
            _tokenContext = tokenContext;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var token = _tokenContext.Token;
            if (!string.IsNullOrEmpty(token))
            {
                var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
                identity = new ClaimsIdentity(claims, "jwt");
            }

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            _tokenContext.Token = token;
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            _tokenContext.Token = null;
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = WebEncoders.Base64UrlDecode(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
    }

}
