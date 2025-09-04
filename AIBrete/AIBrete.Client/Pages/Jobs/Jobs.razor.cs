using AIBrete.Client.Pages.Shared.Jobs;
using AIBrete.Shared.Model;
using AIBrete.Shared.Service.Interfaces;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;

namespace AIBrete.Client.Pages.Jobs
{
    public partial class Job : ComponentBase
    {
        protected string searchTerm = "";
        protected int minCompatibilidad = 60;
        protected bool Asc = false;
        protected string infoCV = "";
        protected string tituloCvModal = "CV PROCESADO";
        protected string tituloDetailModal = "DETALLES DE LA VACANTE";

        [Inject]
        protected IVacanteService VacanteService { get; set; }
        
        [Inject]
        protected ICvService CvService { get; set; }

        [Inject] 
        private IModalService Modal { get; set; } = default!;
        protected IEnumerable<Vacante> VacantesFiltradas { get; set; }
        protected List<Region> Countries { get; set; }

        protected CvData DatosCV { get; set; }
        protected string SelectedCountryCode { get; set; }

        protected bool UsuarioEsPro => true;
        protected bool _loaded = false;

        protected IBrowserFile selectedFile;
        protected bool isFileSelected = false;

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
                var countrySelected = Countries.Where(x => x.Code == SelectedCountryCode).FirstOrDefault();
                VacantesFiltradas = VacantesFiltradas.Where(v => v.Ubicacion == countrySelected.NameEs).ToList();
            }
            _loaded = true;
        }
        protected void OnCvSelected(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
            isFileSelected = selectedFile != null;
        }
        protected async Task UploadCv()
        {
            if (selectedFile == null)
                return;
            _loaded = false;
            DatosCV = await CvService.UploadCvAsync(selectedFile);
            var parameters = new ModalParameters();
            parameters.Add(nameof(CvModal.datosCurriculum), DatosCV);
            _loaded = true;
            Modal.Show<CvModal>(tituloCvModal, parameters);
            isFileSelected = false;
        }

        protected void VerDetalles(Vacante vacanteSelected)
        {
            _loaded = false;
            var parameters = new ModalParameters();
            parameters.Add(nameof(JobDetail.datosVacante), vacanteSelected);
            _loaded = true;
            Modal.Show<JobDetail>(tituloDetailModal, parameters);
        }

        protected void Postular(string titulo)
        {
            Console.WriteLine($"Postulado automáticamente a: {titulo}");
        }
    }

}
