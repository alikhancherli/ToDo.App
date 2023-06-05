using ToDo.App.Domain.Enums;
using ToDo.App.Shared.Application;

namespace ToDo.App.Application.Dto
{
    public class ToDoDto : BaseDto<int>
    {
        public string Title { get; set; }
        public Tag Tag { get; set; }
        public int UserId { get; set; }
        public IList<ToDoItemDto> ToDoItems { get; set; }
    }
}
