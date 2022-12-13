using System.Net.Http.Json;
using BeastBattle.Client.Services.Contracts;
using BeastBattle.Shared.Dtos;

namespace BeastBattle.Client.Services
{
    public class TypeService : ITypeService
    {
        private readonly HttpClient http;
        public TypeService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<PageDto<TypePageDto>?> GetTypes()
        {
            return await this.http.GetFromJsonAsync<PageDto<TypePageDto>>("api/Type");
        }
        public async Task<PageDto<TypePageDto>?> GetType(string name)
        {
            return await this.http.GetFromJsonAsync<PageDto<TypePageDto>>("api/Type/" + name);
        }
    }
}