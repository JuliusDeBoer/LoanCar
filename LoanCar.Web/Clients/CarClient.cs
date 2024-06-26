using LoanCar.Shared.Responses;

namespace LoanCar.Web.Clients
{
	public class CarClient(HttpClient httpClient) : Client(httpClient)
	{
		private readonly string _basePath = "http://localhost:5177/cars";

		public async Task<IEnumerable<PublicCarDTO>> GetCars()
		{
			var result = await Get<IEnumerable<PublicCarDTO>>(_basePath);

			if (result is null)
			{
				throw new Exception("Could not get cars!");
			}

			return result;
		}
		public async Task<PublicCarDTO> GetCar(string id)
		{
			var result = await Get<PublicCarDTO>(_basePath + "/" + id);

			return result is null ? throw new Exception("Could not get car!") : result;
		}
	}
}
