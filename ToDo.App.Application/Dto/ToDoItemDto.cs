using ToDo.App.Shared.Application;

namespace ToDo.App.Application.Dto
{
    public class ToDoItemDto : BaseDto<Guid>
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTimeOffset? Reminder { get; set; }
        public byte PriorityLevel { get; set; }
        public bool Done { get; set; }
    }
}
