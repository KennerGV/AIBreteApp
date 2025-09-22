using AIBrete.Service.Services.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;

namespace AIBrete.Components.Pages.Auth
{
    public partial class Log : ComponentBase
    {
        protected LoginModel loginModel = new();
        protected bool loginError = false;
        protected bool isLoading = false;

        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] public AuthService AuthService { get; set; } = default!;
        protected async Task HandleLogin()
        {
            loginError = false;
            isLoading = true;

            var isAuthenticated = await AuthService.LoginAsync(loginModel.Username, loginModel.Password);

            if (isAuthenticated)
            {
                NavigationManager.NavigateTo("/jobs");
            }
            else
            {
                loginError = true;
            }
            isLoading = false;
        }
        public class LoginModel
        {
            [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            public string Password { get; set; }
        }
    }
}

