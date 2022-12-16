using System.Net.Http.Json;
using BeastBattle.Shared.Services.Contracts;
using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;
using Microsoft.AspNetCore.Components.Authorization;

namespace BeastBattle.Shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient http;
        private readonly AuthenticationStateProvider authStateProvider;
        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            this.http = http;
            this.authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<int>?> Register(RegisterDto request)
        {
            Console.WriteLine(request.Name + " " + request.Password + " " + request.Email);
            var result = await this.http.PostAsJsonAsync("api/auth/register", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<string>?> Login(LoginDto request)
        {
            var result = await this.http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await this.authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}