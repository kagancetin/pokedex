using BeastBattle.Shared.Entities;
namespace BeastBattle.Shared.Dtos
{
    public class PagingDto<T>
    {
        public List<T?>? nodes { get; set; }
        public List<Edges<T>?>? edges { get; set; }
        public PageInfo? pageInfo { get; set; }
        public int? totalCount { get; set; }

    }

    public class PageInfo
    {
        public string? startCursor { get; set; }
        public string? endCursor { get; set; }
        public bool hasNextPage { get; set; }
        public bool hasPreviousPage { get; set; }
    }

    public class Edges<T>
    {
        public string? cursor { get; set; }
        public T? node { get; set; }
    }
}

