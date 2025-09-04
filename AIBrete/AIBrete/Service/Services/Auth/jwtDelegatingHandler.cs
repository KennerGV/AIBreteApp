using AIBrete.Shared.Service.Interfaces.Auth;

namespace AIBrete.Service.Services.Auth
{
    public class JwtDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenContextHolder _tokenContext;
        public JwtDelegatingHandler(ITokenContextHolder tokenContext)
        {
            _tokenContext = tokenContext;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _tokenContext.Token;
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }

}
