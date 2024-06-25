using LoanCar.Shared.Requests;
using LoanCar.Web.Clients;
using Microsoft.AspNetCore.Components;

namespace LoanCar.Web.Pages
{
    public partial class Login
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        private AuthClient AuthClient { get; set; } = default!;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string? _errorMessage;

        private async Task HandleLogin()
        {
            var userDto = new LoginDTO()
            {
                Email = _email,
                Password = _password
            };
            var result = await AuthClient.Login(userDto);

            if (result)
                NavigationManager.NavigateTo("/dashboard");
            else
                _errorMessage = "Ongeldig wachtwoord of email";
        }
    }
}