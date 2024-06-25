using Blazored.LocalStorage;
using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using Microsoft.AspNetCore.Components.Authorization;

namespace LoanCar.Web.Clients
{
    public class AuthClient(ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider, HttpClient httpClient) : Client(httpClient)
    {
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;
        private readonly string _basePath = "http://localhost:5177/auth";

        public async Task<bool> Login(LoginDTO userDto)
        {
            var result = await Put<LoginDTO, AuthTokenDTO>(_basePath + "/login", userDto);

            if (result is null)
                return false;

            await _localStorage.SetItemAsync("token", result.Token);
            await _authStateProvider.GetAuthenticationStateAsync();

            return true;
        }

        // public async Task<bool> Register(LoginDto userDto)
        // {
        // var result = await Post<LoginDto, string?>(_basePath + "/register", userDto);

        // if (result is null)
        // return false;

        // await _localStorage.SetItemAsync("token", result);
        // await _authStateProvider.GetAuthenticationStateAsync();

        // return true;
        // }

        public async Task<bool> Logout()
        {
            try
            {
                await _localStorage.RemoveItemAsync("token");
                await _authStateProvider.GetAuthenticationStateAsync();
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
