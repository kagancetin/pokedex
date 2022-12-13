
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace BeastBattle.Server.Graphs
{
    public class QueryableStringInvariantContainsHandler : QueryableStringOperationHandler
    {
        public QueryableStringInvariantContainsHandler(InputParser inputParser) : base(inputParser)
        {
        }
        // For creating a expression tree we need the `MethodInfo` of the `ToLower` method of string
        private static readonly MethodInfo _contains = typeof(string).GetMethod("IndexOf",
             new[] { typeof(string), typeof(StringComparison) });


        protected override int Operation => DefaultFilterOperations.Contains;
        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object parsedValue)
        {
            Expression property = context.GetInstance();
            if (parsedValue is string str)
            {
                return Expression.NotEqual(
                        Expression.Call(
                            property,
                            _contains,
                            Expression.Constant(str, typeof(string)),
                            Expression.Constant(StringComparison.InvariantCultureIgnoreCase, typeof(StringComparison))
                        ),
                        Expression.Constant(-1, typeof(int))
                    );
            }
            throw new InvalidOperationException();
        }
    }
}
