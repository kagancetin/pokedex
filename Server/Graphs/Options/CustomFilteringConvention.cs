using HotChocolate.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;

namespace BeastBattle.Server.Graphs
{
    public class CustomFilteringConvention : FilterConvention
    {
        protected override void Configure(IFilterConventionDescriptor descriptor)
        {
            descriptor.AddDefaults();
            descriptor.Provider(
                new QueryableFilterProvider(
                     x => x
                .AddFieldHandler<fake>()
                .AddFieldHandler<QueryableStringInvariantEqualsHandler>()
                .AddFieldHandler<QueryableStringInvariantContainsHandler>()
                .AddDefaultFieldHandlers()));
        }
    }
}