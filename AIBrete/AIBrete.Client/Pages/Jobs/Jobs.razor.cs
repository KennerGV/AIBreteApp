using AIBrete.Shared.Model;
using AIBrete.Shared.Service.Interfaces;
using Microsoft.AspNetCore.Components;

namespace AIBrete.Client.Pages.Jobs
{
    public class Job : ComponentBase
    {
        protected string searchTerm = "";
        protected int minCompatibilidad = 60;
        protected bool Asc = false;

        [Inject]
        protected IVacanteService VacanteService { get; set; }

        protected IEnumerable<Vacante> VacantesFiltradas { get; set; }

        protected bool UsuarioEsPro => true;

        protected override void OnInitialized()
        {
            // Inicializar VacantesFiltradas al principio
            VacantesFiltradas = VacanteService.GetVacantes(searchTerm, minCompatibilidad, Asc);
        }

        protected void ActualizarVacantes()
        {
            // Actualizar VacantesFiltradas cuando cambian los filtros
            VacantesFiltradas = VacanteService.GetVacantes(searchTerm, minCompatibilidad, Asc);
        }

        protected void VerDetalles(string titulo)
        {
            Console.WriteLine($"Ver detalles de: {titulo}");
        }

        protected void Postular(string titulo)
        {
            Console.WriteLine($"Postulado automáticamente a: {titulo}");
        }
    }

}
