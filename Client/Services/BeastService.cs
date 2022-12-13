using Microsoft.AspNetCore.Components;
using BeastBattle.Client.Services.Contracts;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace BeastBattle.Client.Services
{

    public class BeastService : IBeastService
    {
        private readonly HttpClient http;
        public BeastService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<JObject> GetBeasts()
        {
            var result = await this.http.GetFromJsonAsync<JObject>("api/beast");
            return result;
        }

    }

}

