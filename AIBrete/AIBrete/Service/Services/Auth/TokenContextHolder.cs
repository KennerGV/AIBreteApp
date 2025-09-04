using AIBrete.Shared.Service.Interfaces.Auth;

namespace AIBrete.Service.Services.Auth
{
    public class TokenContextHolder : ITokenContextHolder
    {
        private static readonly AsyncLocal<string?> _tokenCurrent = new();
        public string? Token
        {
            get => _tokenCurrent.Value;
            set => _tokenCurrent.Value = value;
        }
    }
}
