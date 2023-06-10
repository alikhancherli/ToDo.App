using ToDo.App.Domain.ValueObjects;
using ToDo.App.Shared.Domain;

namespace ToDo.App.Domain.Entities
{
    public sealed class ToDoItem : Entity<Guid>
    {
        public string Title { get; private set; } = default!;
        public string Note { get; private set; } = default!;
        public DateTimeOffset? Reminder { get; private set; }
        public PriorityLevel PriorityLevel { get; private set; } = default!;
        public bool Done { get; set; } = false;

        private ToDoItem()
        {

        }

        public static ToDoItem Create(string title,
            string note,
            DateTimeOffset? reminder,
            PriorityLevel priorityLevel) =>
            new ToDoItem()
            {
                PriorityLevel = priorityLevel,
                Title = title,
                Note = note,
                Reminder = reminder
            };

        public static ToDoItem Edit(
            Guid id,
            string title,
            string note,
            DateTimeOffset? reminder,
            PriorityLevel priorityLevel) =>
            new ToDoItem()
            {
                Id = id,
                PriorityLevel = priorityLevel,
                Title = title,
                Note = note,
                Reminder = reminder
            };

        public void DoneItem() => this.Done = true;
    }
}
