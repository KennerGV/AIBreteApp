namespace AIBrete.Shared.Model
{
    public class Vacante
    {
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Ubicacion { get; set; }
        public int Compatibilidad { get; set; }
        public Vacante(string titulo, string empresa, string ubicacion, int compatibilidad)
        {
            Titulo = titulo;
            Empresa = empresa;
            Ubicacion = ubicacion;
            Compatibilidad = compatibilidad;
        }
    }
}
