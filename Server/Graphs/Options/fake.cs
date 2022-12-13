
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace BeastBattle.Server.Graphs
{
    public class fake : QueryableStringOperationHandler
    {
        public fake(InputParser inputParser) : base(inputParser)
        {
        }
        // For creating a expression tree we need the `MethodInfo` of the `ToLower` method of string

        protected override int Operation => DefaultFilterOperations.EndsWith;
        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object parsedValue)
        {
            MethodInfo containMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", System.Type.EmptyTypes);
            Expression property = context.GetInstance();
            Console.WriteLine(containMethod);
            Console.WriteLine(toLowerMethod);

            if (parsedValue is string str)
            {
                return Expression.Call(Expression.Call(property, toLowerMethod),
                containMethod,
                Expression.Constant(str.ToLower(), typeof(string))
                );
            }
            throw new InvalidOperationException();
        }
    }
}
