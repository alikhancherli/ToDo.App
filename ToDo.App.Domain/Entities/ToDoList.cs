using ToDo.App.Domain.Enums;
using ToDo.App.Domain.Events;
using ToDo.App.Shared.Domain;

namespace ToDo.App.Domain.Entities
{
    public sealed class ToDoList : AggregateRoot<int>
    {
        public string Title { get; private set; } = default!;
        public Tag Tag { get; private set; }
        public int UserId { get; private set; }
        public IEnumerable<ToDoItem> ToDoItems { get; private set; } = Enumerable.Empty<ToDoItem>();

        public ToDoList(
            string title,
            Tag tag,
            int userId,
            IList<ToDoItem> toDoItems)
        {
            Create(title, tag, userId, toDoItems);
        }

        private void Create(
            string title,
            Tag tag,
            int userId,
            IList<ToDoItem> toDoItems)
        {
            Title = title;
            Tag = tag;
            UserId = userId;
            ToDoItems = toDoItems;

            AddEvent(new TodoHasCreatedEvent());
        }

        public void Edit(
            string title,
            Tag tag,
            IList<ToDoItem> toDoItems)
        {
            Title = title;
            Tag = tag;
            ToDoItems = toDoItems;

            ModifiedTimeUtc = DateTimeOffset.UtcNow;
            AddEvent(new TodoHasUpdatedEvent());
        }
    }
}
