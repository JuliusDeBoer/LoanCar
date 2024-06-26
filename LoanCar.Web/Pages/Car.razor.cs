using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using LoanCar.Web.Clients;
using Microsoft.AspNetCore.Components;

namespace LoanCar.Web.Pages
{
    public partial class Car
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        private AuthClient AuthClient { get; set; } = default!;
        [Inject]
        private ReservationClient ReservationClient { get; set; } = default!;

        [Parameter]
        public string? Id { get; set; } = string.Empty;
        [Inject]
        private CarClient CarClient { get; set; } = default!;

        private PublicCarDTO Target { get; set; } = default!;

        private string _beginLocation = string.Empty;
        private string _endLocation = string.Empty;
        private string _start = default!;

        private async Task HandleReservation()
        {
            var dto = new NewReservationDTO()
            {
                CarId = new Guid(Id),
                Origin = _beginLocation,
                Destination = _endLocation,
                UserId = new Guid(), // TODO
                StartingTime = DateTime.Parse(_start),
            };

            await ReservationClient.Reserve(dto);
        }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                return;
            }

            var result = await CarClient.GetCar(Id);

            if (result is not null)
                Target = result;

            await base.OnInitializedAsync();
        }
    }
}