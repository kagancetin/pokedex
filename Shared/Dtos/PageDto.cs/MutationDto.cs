using BeastBattle.Shared.Entities;
namespace BeastBattle.Shared.Dtos
{
    public class MutationDto<T>
    {
        public T? data { get; set; }
        public string? error { get; set; }
        public string? success { get; set; }

    }
}
