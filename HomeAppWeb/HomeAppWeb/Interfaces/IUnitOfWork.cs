using HomeAppWeb.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IEventRepository Events { get; }
        IUserEventRepository UserEvents { get; }
        IBillRepository Bills { get; }
        ITodoRepository Todos { get; }
        ITodoItemRepository TodoItems { get; }
        IRecipeRepository Recipes { get; }
        IActionLogRepository ActionLogs { get; }
        IPersonRepository Persons { get; }
        Task<int> CompleteAsync();
    }
}
