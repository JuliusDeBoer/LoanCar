using Microsoft.AspNetCore.Components;

namespace LoanCar.Web.Pages
{
    public partial class Home
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
    }
}
