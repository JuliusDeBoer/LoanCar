using LoanCar.Shared.Responses;
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

        [Inject]
        private CarClient CarClient { get; set; } = default!;

        private IEnumerable<PublicCarDTO> Cars { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            var result = await CarClient.GetCars();

            if (result is not null)
                Cars = result.ToList();

            await base.OnInitializedAsync();
        }
    }
}