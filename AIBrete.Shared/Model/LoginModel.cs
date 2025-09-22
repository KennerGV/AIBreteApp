using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBrete.Shared.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public required string Password { get; set; }
    }
}
