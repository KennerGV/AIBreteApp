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
        protected List<Region> Countries { get; set; }
        protected string SelectedCountryCode { get; set; }

        protected bool UsuarioEsPro => true;
        protected bool _loaded = false;

        protected override async Task OnInitializedAsync()
        {
            // Inicializar VacantesFiltradas al principio
            Countries = CountryProvider.GetCountries();
            VacantesFiltradas = await VacanteService.GetVacantes(searchTerm, minCompatibilidad, Asc);

            _loaded = true;
        }

        protected async Task ActualizarVacantes()
        {
            // Actualizar VacantesFiltradas cuando cambian los filtros
            VacantesFiltradas = await VacanteService.GetVacantes(searchTerm, minCompatibilidad, Asc);
            if (!string.IsNullOrEmpty(SelectedCountryCode))
            {
                VacantesFiltradas = VacantesFiltradas.Where(v => v.Ubicacion == SelectedCountryCode).ToList();
            }
            _loaded = true;
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
