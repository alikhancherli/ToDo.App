using System.Linq.Expressions;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Domain.Repositories
{
    public interface ITodoRepository
    {
        ValueTask<ToDoList?> GetAsync(Expression<Func<ToDoList, bool>> predicate, CancellationToken cancellationToken);
        ValueTask<ToDoList?> GetAsync(int id, CancellationToken cancellationToken);
        Task<IList<ToDoList>> GetListAsync(Expression<Func<ToDoList, bool>> predicate, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(ToDoList toDoList);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
