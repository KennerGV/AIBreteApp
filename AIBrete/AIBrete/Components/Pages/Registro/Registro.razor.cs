using AIBrete.Model;
using AIBrete.Service.Services.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;

namespace AIBrete.Components.Pages.Registro
{
    public partial class Registry : ComponentBase
    {
        protected UserModel userModel = new();
        protected RegisterResponse result = new();

        //Errores dinámicos en validaciones de contraseña
        protected string passwordErrors = string.Empty;

        //Error de comparación en campo de confirmación de contraseña para mostrarse en tiempo real de digitación de contraseña
        protected string passwordMatchError = string.Empty;


        protected bool emailTouched = false;
        protected string emailError = string.Empty;

        protected bool loginError = false;
        protected bool isLoading = false;

        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] public AuthService AuthService { get; set; } = default!;

        protected void ValidatePassword(ChangeEventArgs e)
        {
            userModel.Password = e.Value?.ToString() ?? string.Empty;

            var context = new ValidationContext(userModel);
            UserModel.ValidatePassword(userModel.Password, context);

            StateHasChanged(); // actualiza la UI
        }
        protected void ValidatePasswordMatch(ChangeEventArgs e)
        {
            userModel.ConfirmPassword = e.Value?.ToString() ?? string.Empty;

            if (userModel.ConfirmPassword != null) 
            {
                passwordMatchError = userModel.Password != userModel.ConfirmPassword
                ? ""
                : string.Empty;
            }            
        }


        protected async Task HandleValidSubmit()
        {
            isLoading = true;
            result = await AuthService.RegisterAsync(userModel);

            if (result.Success)
            {
                NavigationManager.NavigateTo("/jobs");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                loginError = true;
            }

            isLoading = false;
        }
    }
}
