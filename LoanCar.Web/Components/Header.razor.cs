using LoanCar.Web.Clients;
using Microsoft.AspNetCore.Components;

namespace LoanCar.Web.Components
{
    public partial class Header
    {
        [Inject]
        private AuthClient AuthClient { get; set; } = default!;

        public async Task Logout()
        {
            await AuthClient.Logout();
        }
    }
}