using HotChocolate.Execution;

namespace BeastBattle.Server.Services.Contracts
{
    public interface IExecuteGraph
    {
        Task<IExecutionResult> Execute(string query);
    }
}