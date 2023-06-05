using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDo.App.Domain.Entities;
using ToDo.App.Domain.Repositories;

namespace ToDo.App.Infrastructure.Persistence.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            _context = context;
        }

        public async Task AddAsync(ToDoList toDoList)
        {
            await _context.TodoLists.AddAsync(toDoList);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var todo = await GetAsync(id, cancellationToken);
            if (todo is null)
                return false;
            _context.Remove(todo);
            return true;
        }

        public async ValueTask<ToDoList?> GetAsync(Expression<Func<ToDoList, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.TodoLists.AsNoTracking().SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public async ValueTask<ToDoList?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.TodoLists.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IList<ToDoList>> GetListAsync(Expression<Func<ToDoList, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.TodoLists
                .AsNoTracking()
                .Include(t => t.ToDoItems)
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(ToDoList toDoList)
        {
            if (_context.Entry(toDoList).State is not EntityState.Modified)
                _context.TodoLists.Update(toDoList);

            return await Task.FromResult(true);
        }
    }
}
