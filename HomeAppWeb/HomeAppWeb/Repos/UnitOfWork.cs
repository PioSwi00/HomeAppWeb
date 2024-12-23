using HomeAppWeb.Data;
using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Repositories;
using System.Threading.Tasks;

namespace HomeAppWeb.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Events = new EventRepository(_context);
            UserEvents = new UserEventRepository(_context);
            Bills = new BillRepository(_context);
            Todos = new TodoRepository(_context);
            TodoItems = new TodoItemRepository(_context);
            Recipes = new RecipeRepository(_context);
            ActionLogs = new ActionLogRepository(_context);
            Persons = new PersonRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IEventRepository Events { get; private set; }
        public IUserEventRepository UserEvents { get; private set; }
        public IBillRepository Bills { get; private set; }
        public ITodoRepository Todos { get; private set; }
        public ITodoItemRepository TodoItems { get; private set; }
        public IRecipeRepository Recipes { get; private set; }
        public IActionLogRepository ActionLogs { get; private set; }
        public IPersonRepository Persons { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

