using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using BeastBattle.Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace BeastBattle.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient http;


        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            this.localStorageService = localStorageService;
            this.http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authToken = await this.localStorageService.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            this.http.DefaultRequestHeaders.Authorization = null;



            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    //Check token expired time
                    var claims = ParseClaimsFromJwt(authToken);
                    var expiry = claims.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();
                    var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));
                    if (datetime.UtcDateTime <= DateTime.UtcNow)
                        throw new InvalidOperationException("The token expired.");
                    //Check token expired time
                    this.http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));

                }
                catch
                {
                    await this.localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer
                .Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }


    }
}