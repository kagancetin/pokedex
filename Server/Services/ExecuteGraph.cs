using BeastBattle.Server.Services.Contracts;
using HotChocolate.Execution;

namespace BeastBattle.Server.Services
{
    public class ExecuteGraph : IExecuteGraph
    {
        private readonly IRequestExecutorResolver resolver;

        public ExecuteGraph(IRequestExecutorResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<IExecutionResult> Execute(string query)
        {
            IRequestExecutor executor = await resolver.GetRequestExecutorAsync();
            IQueryRequest request = new QueryRequestBuilder()
                                        .SetQuery(query)
                                        .Create();
            IExecutionResult result = await executor.ExecuteAsync(request);
            return result;
        }
    }
}