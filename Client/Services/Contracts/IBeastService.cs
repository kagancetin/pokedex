using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;
using Newtonsoft.Json.Linq;

namespace BeastBattle.Client.Services.Contracts
{
    public interface IBeastService
    {
        Task<JObject> GetBeasts();
        //Task<string> CreateType(JArray types);
    }
}