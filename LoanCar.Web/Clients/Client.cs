using System.Net.Http.Json;

namespace LoanCar.Web.Clients
{
    public class Client(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        protected async Task<T?> Get<T>(string path)
        {
            try
            {
                var response = await _httpClient.GetAsync(path);
                var result = await response.Content.ReadFromJsonAsync<T>();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        protected async Task<TResult?> Post<T, TResult>(string path, T data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(path, data);

                TResult? result;
                if (typeof(TResult) == typeof(string))
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    result = (TResult)(object)responseString;
                }
                else
                {
                    result = await response.Content.ReadFromJsonAsync<TResult>();
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        protected async Task<TResult?> Put<T, TResult>(string path, T data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(path, data);
                var result = await response.Content.ReadFromJsonAsync<TResult>();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        protected async Task<bool> Delete(string path)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(path);

                if (!response.IsSuccessStatusCode)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
