using System.ComponentModel.DataAnnotations;

namespace AIBrete.Model
{
    public class UserModel
    {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email del usuario es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public List<string> PasswordErrors { get; set; } = new();

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Para continuar es necesario que acepte los términos.")]
        public bool AgreeTerms { get; set; }


        public static ValidationResult? ValidatePassword(string password, ValidationContext context)
        {
            var instance = (UserModel)context.ObjectInstance;
            instance.PasswordErrors.Clear();

            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult("La contraseña es obligatoria.");

            if (password.Length < 8)
                instance.PasswordErrors.Add("Debe tener al menos 8 caracteres.");
            if (!password.Any(char.IsUpper))
                instance.PasswordErrors.Add("Debe incluir al menos una letra mayúscula.");
            if (!password.Any(char.IsLower))
                instance.PasswordErrors.Add("Debe incluir al menos una letra minúscula.");
            if (!password.Any(char.IsDigit))
                instance.PasswordErrors.Add("Debe incluir al menos un número.");
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                instance.PasswordErrors.Add("Debe incluir al menos un símbolo.");

            return instance.PasswordErrors.Any()
                ? new ValidationResult("La contraseña no cumple con los requisitos.")
                : ValidationResult.Success;
        }

    }
}
