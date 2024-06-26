using LoanCar.Shared.Requests;

namespace LoanCar.Web.Clients
{
    public class ReservationClient(HttpClient httpClient) : Client(httpClient)
    {
        private readonly string _basePath = "http://localhost:5177/reservations";

        public async Task Reserve(NewReservationDTO dto)
        {
            await Post<NewReservationDTO, string?>(_basePath, dto);
        }
    }
}
