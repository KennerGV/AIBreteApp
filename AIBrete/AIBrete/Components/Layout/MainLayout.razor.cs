using Microsoft.AspNetCore.Components;

namespace AIBrete.Components.Layout
{
    public partial class MainLayout
    {
        //Gets context that contains auth state
        [CascadingParameter]
        private HttpContext? HttpContext { get; set; }

        //Injects Nav Man to redirect
        [Inject] private NavigationManager? NavMan { get; set; }


        /// <summary>
        /// On Init
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            //Check if user is logged in
            //if (HttpContext == null || HttpContext.User.Identity.IsAuthenticated == false && HttpContext.Request.Path != "/Account/Login")
            //{
            //Redirects if not logged in
            //NavMan!.NavigateTo("login", true);
            //}

            //StateHasChanged();
        }

    }
}
