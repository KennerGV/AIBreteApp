namespace AIBrete.Model
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
