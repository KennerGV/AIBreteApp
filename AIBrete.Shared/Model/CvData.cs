using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBrete.Shared.Model
{
    public class CvData
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? sobreMi { get; set; }
        public List<string> Habilidades { get; set; } = new();
        public List<string> Experiencia { get; set; } = new();
        public List<string> Educacion { get; set; } = new();
        public List<string> Cursos { get; set; } = new();
    }

}
