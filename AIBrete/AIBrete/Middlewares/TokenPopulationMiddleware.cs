using AIBrete.Shared.Service.Interfaces.Auth;

namespace AIBrete.Middlewares
{
    public class TokenPopulationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenContextHolder _tokenContext;
        public TokenPopulationMiddleware(RequestDelegate next, ITokenContextHolder tokenContext)
        {
            _next = next;
            _tokenContext = tokenContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Ejemplo: token obtenido desde la sesión o una cookie
            string? token = context.Request.Cookies["AuthToken"];
            _tokenContext.Token = token;
            await _next(context);
        }
    }
}
