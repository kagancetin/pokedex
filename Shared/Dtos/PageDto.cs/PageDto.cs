using BeastBattle.Shared.Entities;
namespace BeastBattle.Shared.Dtos
{
    public class PageDto<T>
    {
        public T? data { get; set; }
        public List<ErrorDto?>? errors { get; set; }

    }

    public class ErrorDto
    {
        public string? message { get; set; }
        /*
        public List<object?>? locations { get; set; }
        public List<object?>? path { get; set; }
        public object? extensions { get; set; }*/
    }
}

