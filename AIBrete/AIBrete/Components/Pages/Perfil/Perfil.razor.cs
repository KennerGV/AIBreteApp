using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AIBrete.Components.Pages.Perfil
{
    public partial class Profile : ComponentBase
    {
        public class UserModel
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Location { get; set; }
            public string Phone { get; set; }
            public string ExperienceSummary { get; set; }
            public string Description { get; set; }
        }
        public string fileName { get; set; }

        protected UserModel user = new();

        protected string CvUpload;
        protected IBrowserFile selectedFile;
        protected bool isFileSelected = false;

        protected void HandleValidSubmit()
        {
            // lógica para guardar cambios
        }

        protected void HandleImageUpload(InputFileChangeEventArgs e)
        {
            // lógica para subir imagen
        }

        protected void OnCvSelected(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
            isFileSelected = selectedFile != null;

            fileName = selectedFile.Name.ToString();
        }

        protected async Task HandleCVUpload()
        {
            if (selectedFile == null)
                return;
            // lógica para subir CV
        }
    }
}
