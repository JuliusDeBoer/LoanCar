using LoanCar.Web.Clients;
using Microsoft.AspNetCore.Components;

namespace LoanCar.Web.Pages
{
    public partial class Dashboard
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        private AuthClient AuthClient { get; set; } = default!;
    }
}